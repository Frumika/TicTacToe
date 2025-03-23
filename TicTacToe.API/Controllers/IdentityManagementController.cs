﻿using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Requests;
using TicTacToe.Data.Context; // UsersDbContext


[ApiController]
[Route("api/identity")]
public class IdentityManagementController : ControllerBase
{
    private readonly UsersDbContext _dbContext;

    public IdentityManagementController(UsersDbContext dbContext)
    {
        Console.WriteLine("We using IdentityManagementController");
        _dbContext = dbContext;
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
        }
        catch (Exception exception)
        {
            return StatusCode(500, new { message = "Error while authorization user", error = exception.Message });
        }

        return Ok(new { success = "The user was authorized" });
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


    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 10);
    
    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}