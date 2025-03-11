using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;
using TicTacToe.API.Requests;


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