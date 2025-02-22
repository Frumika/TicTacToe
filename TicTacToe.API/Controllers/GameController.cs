using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        Console.WriteLine($"Reques: {request.Row} : {request.Column}");
        return Ok(new { success = true });
    }
}

public class MoveRequest
{
    public int Row { get; set; }
    public int Column { get; set; }
}