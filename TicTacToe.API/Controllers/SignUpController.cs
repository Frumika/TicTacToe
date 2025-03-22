using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Requests;
using TicTacToe.Data.Context; // UsersDbContext


[ApiController]
[Route("api/signup")]
public class SignUpController : ControllerBase
{
    private readonly UsersDbContext _dbContext;

    public SignUpController(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

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

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);
}