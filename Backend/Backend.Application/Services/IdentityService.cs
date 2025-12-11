using Microsoft.EntityFrameworkCore;
using Backend.Application.DTO.Entities.Identity;
using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;
using Backend.Application.Enums;
using Backend.Application.Managers;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Context;
using Backend.Domain.Models.App;


namespace Backend.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UsersDbContext _usersDbContext;
    private readonly UserSessionManager _userSessionManager;

    public IdentityService(UsersDbContext usersDbContext, UserSessionManager userSessionManager)
    {
        _usersDbContext = usersDbContext;
        _userSessionManager = userSessionManager;
    }

    public async Task<IdentityResponse> GetUserByLoginAsync(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return IdentityResponse.Fail(IdentityStatusCode.InvalidLogin, "Login cannot be null or empty");

        try
        {
            User? user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == login);

            return user is null
                ? IdentityResponse.Fail(IdentityStatusCode.UserNotFound, "User not found")
                : IdentityResponse.Success(new UserDto(user));
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<IdentityResponse> GetUsersListAsync(GetUsersListRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var users = await _usersDbContext.Users.AsNoTracking()
                .OrderBy(user => user.Login)
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount + 1)
                .Select(user => new UserDto(user))
                .ToListAsync();

            bool isLastPage = users.Count <= request.UsersCount;
            if (!isLastPage) users.RemoveAt(users.Count - 1);

            return IdentityResponse.Success(new UsersListDto { Users = users, IsLastPage = isLastPage });
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<IdentityResponse> ChangeUserDataAsync(ChangeUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Login == request.OldLogin);
            if (user is null) return IdentityResponse.Fail(IdentityStatusCode.UserNotFound, "User not found");

            var existing = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Login == request.NewLogin);
            if (existing is not null && request.NewLogin != user.Login)
                return IdentityResponse
                    .Fail(IdentityStatusCode.UserAlreadyExists, "User with this login already exists");

            user.Login = request.NewLogin;
            user.Wins = request.Wins;
            user.Losses = request.Losses;
            user.Draws = request.Draws;
            user.Matches = request.Wins + request.Losses + request.Draws;

            _usersDbContext.Users.Update(user);
            await _usersDbContext.SaveChangesAsync();

            return IdentityResponse.Success(new UserDto(user), "The user data was updated");
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<IdentityResponse> UpdateUserStatsAsync(UpdateUserStatsRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null)
                return IdentityResponse.Fail(IdentityStatusCode.UserNotFound, "The user is not logged in");

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
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }

        return IdentityResponse.Success("The user data was updated");
    }

    public async Task<IdentityResponse> GetUsersStatsAsync(GetUsersStatisticsRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

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
                return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, "Unknown statistics type");
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

            return IdentityResponse.Success(usersStatisticDto);
        }
        catch (Exception exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<IdentityResponse> GetUserDataAsyncBySessionId(GetUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            UserRedisDto? userDto = await _userSessionManager.GetSessionAsync(request.SessionId);
            if (userDto is null)
                return IdentityResponse.Fail(IdentityStatusCode.UserNotFound, "The user is not logged in");

            return IdentityResponse.Success(new UserDto { Login = userDto.Login });
        }
        catch (Exception exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<IdentityResponse> SignInUserAsync(IdentityRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, "Invalid login or password");
            }

            string sessionId = Guid.NewGuid().ToString();
            UserRedisDto userDto = new() { Login = user.Login };

            await _userSessionManager.SetSessionAsync(sessionId, userDto);

            return IdentityResponse.Success(new UserSessionDto { SessionId = sessionId });
        }
        catch (Exception exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<IdentityResponse> SignUpUserAsync(IdentityRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            bool isUserExist = await _usersDbContext.Users.AsNoTracking().AnyAsync(user => user.Login == request.Login);
            if (isUserExist)
                return IdentityResponse.Fail(IdentityStatusCode.UserAlreadyExists,
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
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }

        return IdentityResponse.Success("The user was registered");
    }

    public async Task<IdentityResponse> SignOutUserAsync(SignOutRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            bool isDelete = await _userSessionManager.DeleteSessionAsync(request.SessionId);
            if (!isDelete)
                return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "Error while deleting user");
        }

        return IdentityResponse.Success("The user was logged out");
    }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}