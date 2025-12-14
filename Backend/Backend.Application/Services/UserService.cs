using Backend.Application.DTO.Entities.User;
using Backend.Application.DTO.Requests.User;
using Microsoft.EntityFrameworkCore;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Managers;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Context;
using Backend.Domain.Models.App;


namespace Backend.Application.Services;

public class UserService : IUserService
{
    private readonly UsersDbContext _usersDbContext;
    private readonly UserSessionManager _userSessionManager;

    public UserService(UsersDbContext usersDbContext, UserSessionManager userSessionManager)
    {
        _usersDbContext = usersDbContext;
        _userSessionManager = userSessionManager;
    }


    public async Task<UserResponse> UpdateUserStatsAsync(UpdateUserStatsRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null)
                return UserResponse.Fail(UserStatusCode.UserNotFound, "The user is not logged in");

            user.Matches++;

            switch (request.Type)
            {
                case EndGameType.Win:
                {
                    user.Wins++;
                    break;
                }

                case EndGameType.Lose:
                {
                    user.Losses++;
                    break;
                }

                case EndGameType.Draw:
                {
                    user.Draws++;
                    break;
                }
            }

            _usersDbContext.Users.Update(user);

            await _usersDbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, exception.Message);
        }

        return UserResponse.Success("The user data was updated");
    }

    public async Task<UserResponse> GetUsersStatsAsync(GetUsersStatisticsRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        IQueryable<User> query = _usersDbContext.Users.AsNoTracking();

        switch (request.Type)
        {
            case StatisticType.ByMatches:
            {
                query = query.OrderByDescending(user => user.Matches);
                break;
            }
            case StatisticType.ByWins:
            {
                query = query.OrderByDescending(user => user.Wins);
                break;
            }
            case StatisticType.ByLosses:
            {
                query = query.OrderByDescending(user => user.Losses);
                break;
            }
            default:
            {
                return UserResponse.Fail(UserStatusCode.IncorrectData, "Unknown statistics type");
            }
        }

        try
        {
            UsersStatisticDto usersStatisticDto = new();

            var users = await query
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount)
                .Select(user => new UserDto(user))
                .ToListAsync();

            usersStatisticDto.Users = users;
            usersStatisticDto.IsLastPage = users.Count < request.UsersCount;

            return UserResponse.Success(usersStatisticDto);
        }
        catch (Exception exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<UserResponse> GetUserDataAsyncBySessionId(GetUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        try
        {
            var state = await _userSessionManager.GetSessionAsync(request.SessionId);
            if (state is null)
                return UserResponse.Fail(UserStatusCode.UserNotFound, "The user is not logged in");

            return UserResponse.Success(new UserDto { Login = state.Login });
        }
        catch (Exception exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<UserResponse> SignInUserAsync(UserRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return UserResponse.Fail(UserStatusCode.IncorrectData, "Invalid login or password");
            }

            string sessionId = Guid.NewGuid().ToString();
            var sessionState = new UserSessionStateDto
            {
                UserId = user.Id,
                Login = user.Login,
                CreatedAt = DateTime.UtcNow
            };

            await _userSessionManager.SetSessionAsync(sessionId, sessionState);
            return UserResponse.Success(new UserSessionDto { SessionId = sessionId });
        }
        catch (Exception exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<UserResponse> SignUpUserAsync(UserRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        try
        {
            bool isUserExist = await _usersDbContext.Users.AsNoTracking().AnyAsync(user => user.Login == request.Login);
            if (isUserExist)
                return UserResponse.Fail(UserStatusCode.UserAlreadyExists,
                    "User with this login already exists");


            User newUser = new()
            {
                Login = request.Login,
                HashPassword = HashPassword(request.Password)
            };
            _usersDbContext.Users.Add(newUser);

            await _usersDbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, exception.Message);
        }

        return UserResponse.Success("The user was registered");
    }

    public async Task<UserResponse> LogoutUserSessionAsync(LogoutUserSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UserResponse.Fail(UserStatusCode.IncorrectData, result.Message);

        try
        {
            bool isLogout = await _userSessionManager.LogoutSessionAsync(request.SessionId);
            
            return isLogout
                ? UserResponse.Success("The user was logged out")
                : UserResponse.Fail(UserStatusCode.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, "Error while deleting user");
        }
    }

    public async Task<UserResponse> LogoutAllUserSessionsAsync(int userId)
    {
        if (userId <= 0)
            return UserResponse.Fail(UserStatusCode.IncorrectData, "User ID must be greater than 0");

        try
        {
            bool isLogout = await _userSessionManager.LogoutAllSessionsAsync(userId);

            return isLogout
                ? UserResponse.Success("All user sessions were deleted")
                : UserResponse.Fail(UserStatusCode.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, "Error while deleting user");
        }
    }


    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}