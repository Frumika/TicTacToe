using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Requests;
using TicTacToe.Data.Context; // UsersDbContext


[ApiController]
[Route("api/signin")]
public class SignInController : ControllerBase
{
    private readonly UsersDbContext _dbContext;

    public SignInController(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("authorization")]
    public async Task<IActionResult> AuthorizeUser([FromBody] AuthorizeRequest request)
    {
        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Login == request.Login);

            if (user is null || !VerifyPassword(request.Password, user.HashPassword))
            {
                return Unauthorized(new { message = "Invalid login or password" });
            }
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while authorization user", error = exception.Message });
        }

        return Ok(new { success = "The user was authorized" });
    }

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}