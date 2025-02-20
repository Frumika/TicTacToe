using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        Console.WriteLine($"Был сделан запрос: Row: {request.Row} - Column: {request.Col}");
        return Ok(new { success = true });
    }
}

public class MoveRequest
{
    public int Row { get; set; }
    public int Col { get; set; }
}