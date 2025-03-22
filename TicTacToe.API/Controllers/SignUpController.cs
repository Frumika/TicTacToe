using Microsoft.AspNetCore.Mvc; 
using TicTacToe.Services;
using TicTacToe.API.Requests;


[ApiController]
[Route("api/signup")]
public class SignUpController : ControllerBase
{
    [HttpPost("registration")]
    public IActionResult RegisterUser([FromBody] RegisterRequest request)
    {
        Console.WriteLine($"{request.Login}, {request.Password}");
        return Ok(new { success = "User was registered" });
    }
}