using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;


[ApiController]
[Route("api/signup")]
public class SignUpController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult RegiserUser([FromBody] RegisterRequest request)
    {
        
        return Ok(new { success = "User was registered" });
    }
}


public class RegisterRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}