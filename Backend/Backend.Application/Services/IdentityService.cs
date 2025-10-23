using Microsoft.EntityFrameworkCore;

using Backend.Application.DTO.Entities;
using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Context;
using Backend.Domain.Models.App;
using Backend.Services.Redis;


namespace Backend.Application.Services;

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
        var result = request.Validate();
        if (!result.IsValid) return UpdateDataResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null)
                return UpdateDataResponse.Fail(IdentityStatusCode.UserNotFound, "The user is not logged in");

            user.Matches++;
            if (request.IsWin) user.Wins++;
            _usersDbContext.Users.Update(user);

            await _usersDbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return UpdateDataResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }

        return UpdateDataResponse.Success("The user data was updated");
    }

    public async Task<UsersStatisticsResponse> GetUsersStatisticsAsync(GetUsersStatisticsRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return UsersStatisticsResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

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
                return UsersStatisticsResponse.Fail(IdentityStatusCode.IncorrectData, "Unknown statistics type");
            }
        }

        try
        {
            var users = await query
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount)
                .Select(user => new UserDto(user))
                .ToListAsync();

            bool isLastPage = users.Count < request.UsersCount;

            return UsersStatisticsResponse.Success(users, isLastPage);
        }
        catch (Exception exception)
        {
            return UsersStatisticsResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<GetUserDataResponse> GetUserDataAsync(GetUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GetUserDataResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            UserRedisDto? userDto = await _redisSessionService.GetSessionAsync<UserRedisDto>(request.SessionId);
            if (userDto is null)
                return GetUserDataResponse.Fail(IdentityStatusCode.UserNotFound, "The user is not logged in");

            return GetUserDataResponse.Success(new UserDto { Login = userDto.Login });
        }
        catch (Exception exception)
        {
            return GetUserDataResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<SignInResponse> SignInUserAsync(IdentityRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return SignInResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return SignInResponse.Fail(IdentityStatusCode.IncorrectData, "Invalid login or password");
            }

            string sessionId = Guid.NewGuid().ToString();
            UserRedisDto userDto = new() { Login = user.Login };

            await _redisSessionService.SetSessionAsync(sessionId, userDto, TimeSpan.FromMinutes(5));

            return SignInResponse.Success(sessionId);
        }
        catch (Exception exception)
        {
            return SignInResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }
    }

    public async Task<SignUpResponse> SignUpUserAsync(IdentityRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return SignUpResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            bool isUserExist = await _usersDbContext.Users.AsNoTracking().AnyAsync(user => user.Login == request.Login);
            if (isUserExist)
                return SignUpResponse.Fail(IdentityStatusCode.UserAlreadyExists, "User with this login already exists");


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
            return SignUpResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }

        return SignUpResponse.Success("The user was registered");
    }

    public async Task<SignOutResponse> SignOutUserAsync(SignOutRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return SignOutResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            bool isDelete = await _redisSessionService.DeleteSessionAsync(request.SessionId);
            if (!isDelete)
                return SignOutResponse.Fail(IdentityStatusCode.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return SignOutResponse.Fail(IdentityStatusCode.UnknownError, "Error while deleting user");
        }

        return SignOutResponse.Success("The user was logged out");
    }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}