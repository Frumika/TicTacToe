using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.DTO.Requests.Identity;
using TicTacToe.Application.DTO.Responses.Identity;
using TicTacToe.Application.Enums;
using TicTacToe.Application.Interfaces;
using TicTacToe.DataAccess.Context;
using TicTacToe.Domain.Models.App;
using TicTacToe.Services.Redis;
using UserDto = TicTacToe.Application.DTO.Entities.UserDto;

namespace TicTacToe.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UsersDbContext _usersDbContext;
    private readonly RedisSessionService _redisSessionService;

    public IdentityService(UsersDbContext usersDbContext, RedisSessionService redisSessionService)
    {
        _usersDbContext = usersDbContext;
        _redisSessionService = redisSessionService;
    }

    public async Task<UpdateDataResponse> UpdateUserDataAsync(UpdateDataRequest request)
    {
        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null)
                return UpdateDataResponse.Fail(IdentityStatus.UserNotFound, "The user is not logged in");

            user.Matches++;
            if (request.IsWin) user.Wins++;
            _usersDbContext.Users.Update(user);

            await _usersDbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return UpdateDataResponse.Fail(IdentityStatus.UnknownError, exception.Message);
        }

        return UpdateDataResponse.Success("The user data was updated");
    }

    public async Task<UsersStatisticsResponse> GetUsersStatisticsAsync(UsersStatisticsRequest request)
    {
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
                query = query.OrderByDescending(user => user.Matches - user.Wins);
                break;
            }
            default:
            {
                return UsersStatisticsResponse.Fail(IdentityStatus.IncorrectData, "Unknown statistics type");
            }
        }

        try
        {
            var list = await query
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount)
                .Select(user => new UserDto(user))
                .ToListAsync();

            bool isLastPage = list.Count < request.UsersCount;

            return UsersStatisticsResponse.Success(list, isLastPage);
        }
        catch (Exception exception)
        {
            return UsersStatisticsResponse.Fail(IdentityStatus.UnknownError, exception.Message);
        }
    }

    public async Task<GetUserDataResponse> GetUserDataAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return GetUserDataResponse.Fail(IdentityStatus.IncorrectData, "Session ID cannot be null or empty");
        }

        try
        {
            UserRedisDto? userDto = await _redisSessionService.GetSessionAsync<UserRedisDto>(sessionId);
            if (userDto is null)
                return GetUserDataResponse.Fail(IdentityStatus.UserNotFound, "The user is not logged in");

            return GetUserDataResponse.Success(new UserDto { Login = userDto.Login });
        }
        catch (Exception exception)
        {
            return GetUserDataResponse.Fail(IdentityStatus.UnknownError, exception.Message);
        }
    }

    public async Task<SignInResponse> SignInUserAsync(IdentityRequest request)
    {
        try
        {
            var user = await _usersDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return SignInResponse.Fail(IdentityStatus.IncorrectData, "Invalid login or password");
            }

            string sessionId = Guid.NewGuid().ToString();
            UserRedisDto userDto = new() { Login = user.Login };

            await _redisSessionService.SetSessionAsync(sessionId, userDto, TimeSpan.FromMinutes(5));

            return SignInResponse.Success(sessionId);
        }
        catch (Exception exception)
        {
            return SignInResponse.Fail(IdentityStatus.UnknownError, exception.Message);
        }
    }

    public async Task<SignUpResponse> SignUpUserAsync(IdentityRequest request)
    {
        try
        {
            bool isUserExist = await _usersDbContext.Users.AsNoTracking().AnyAsync(user => user.Login == request.Login);
            if (isUserExist)
                return SignUpResponse.Fail(IdentityStatus.IncorrectData, "User with this login already exists");


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
            return SignUpResponse.Fail(IdentityStatus.UnknownError, exception.Message);
        }

        return SignUpResponse.Success("The user was registered");
    }

    public async Task<SignOutResponse> SignOutUserAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return SignOutResponse.Fail(IdentityStatus.IncorrectData, "Session ID cannot be null or empty");
        }

        try
        {
            bool isDelete = await _redisSessionService.DeleteSessionAsync(sessionId);
            if (!isDelete)
                return SignOutResponse.Fail(IdentityStatus.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return SignOutResponse.Fail(IdentityStatus.UnknownError, "Error while deleting user");
        }

        return SignOutResponse.Success("The user was logged out");
    }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}