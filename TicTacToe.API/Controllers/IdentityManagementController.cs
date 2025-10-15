using System.Diagnostics.CodeAnalysis;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Requests;
using TicTacToe.DataAccess.Context;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.App; // UsersDbContext
using TicTacToe.Services.Redis;


[ApiController]
[Route("api/identity")]
public class IdentityManagementController : ControllerBase
{
    private readonly UsersDbContext _dbContext;
    private readonly RedisSessionService _redisSessionService;

    public IdentityManagementController(UsersDbContext dbContext, RedisSessionService redisSessionService)
    {
        _dbContext = dbContext;
        _redisSessionService = redisSessionService;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateDataRequest request)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null) return BadRequest(new { message = "The user is not logged in" });

            user.Matches++;
            if (request.IsWin) user.Wins++;
            _dbContext.Users.Update(user);
            
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while update user info", error = exception.Message });
        }

        return Ok();
    }

    [HttpPost("statistics")]
    public async Task<IActionResult> GetUsersStatistics([FromBody] UsersStatisticsRequest request)
    {
        IQueryable<User> query = _dbContext.Users.AsNoTracking();

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
            default: return BadRequest("Unknown statistics type");
        }

        try
        {
            var list = await query
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount)
                .Select(u => new
                {
                    Login = u.Login,
                    Matches = u.Matches,
                    Wins = u.Wins,
                    Losses = u.Matches - u.Wins
                })
                .ToListAsync();

            return Ok(new { list = list, isLastPage = list.Count < request.UsersCount });
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Internal server error", error = exception.Message });
        }
    }

    [HttpPost("info")]
    public async Task<IActionResult> GetUserData([FromBody] string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return BadRequest(new { message = "Session ID cannot be null or empty" });
        }

        try
        {
            UserDto? userDto = await _redisSessionService.GetSessionAsync<UserDto>(sessionId);
            if (userDto is null) return NotFound(new { message = "The user was not found" });

            return Ok(new { user = userDto });
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error when searching for a user", error = exception.Message });
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] IdentityRequest request)
    {
        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return Unauthorized(new { message = "Invalid login or password" });
            }

            string sessionId = Guid.NewGuid().ToString();
            UserDto userDto = new() { Login = user.Login };

            await _redisSessionService.SetSessionAsync(sessionId, userDto, TimeSpan.FromMinutes(5));

            return Ok(new { sessionId });
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while authorization user", error = exception.Message });
        }
    }


    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser([FromBody] IdentityRequest request)
    {
        try
        {
            bool isUserExist = await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Login == request.Login);
            if (isUserExist) return Conflict(new { message = "User with this login already exists" });

            User newUser = new()
            {
                Login = request.Login,
                HashPassword = HashPassword(request.Password)
            };
            _dbContext.Users.Add(newUser);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while registering user", error = exception.Message });
        }

        return Ok(new { success = "The user was registered" });
    }


    [HttpPost("signout")]
    public async Task<IActionResult> SignOutUser([FromBody] string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return BadRequest(new { message = "Session ID cannot be null or empty" });
        }

        try
        {
            bool isDelete = await _redisSessionService.DeleteSessionAsync(sessionId);
            if (!isDelete) return StatusCode(500, new { message = "The user has not been deleted" });
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while deleting user", error = exception.Message });
        }

        return NoContent();
    }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}