using Microsoft.EntityFrameworkCore;
using Backend.Application.DTO.Entities.Identity;
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

    public async Task<IdentityResponse> UpdateUserDataAsync(UpdateDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null)
                return IdentityResponse.Fail(IdentityStatusCode.UserNotFound, "The user is not logged in");

            user.Matches++;
            if (request.IsWin) user.Wins++;
            _usersDbContext.Users.Update(user);

            await _usersDbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, exception.Message);
        }

        return IdentityResponse.Success(null, "The user data was updated");
    }

    public async Task<IdentityResponse> GetUsersStatisticsAsync(GetUsersStatisticsRequest request)
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
                query = query.OrderByDescending(user => user.Matches - user.Wins);
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

    public async Task<IdentityResponse> GetUserDataAsync(GetUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            UserRedisDto? userDto = await _redisSessionService.GetSessionAsync<UserRedisDto>(request.SessionId);
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

            await _redisSessionService.SetSessionAsync(sessionId, userDto, TimeSpan.FromMinutes(5));

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

        return IdentityResponse.Success(null, "The user was registered");
    }

    public async Task<IdentityResponse> SignOutUserAsync(SignOutRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return IdentityResponse.Fail(IdentityStatusCode.IncorrectData, result.Message);

        try
        {
            bool isDelete = await _redisSessionService.DeleteSessionAsync(request.SessionId);
            if (!isDelete)
                return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "The user has not been deleted");
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(IdentityStatusCode.UnknownError, "Error while deleting user");
        }

        return IdentityResponse.Success(null, "The user was logged out");
    }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}