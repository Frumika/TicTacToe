using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;


[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private GameSessionsService _gameSessionsService;

    public GameController(GameSessionsService gameSessionsService)
    {
        _gameSessionsService = gameSessionsService;
    }

    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        Console.WriteLine($"Request: id = {request.SessionId} ({request.Row} : {request.Column})");

        var session = _gameSessionsService.GetOrCreateSession(request.SessionId);
        bool moveSuccess = session.SendRequest(request.Row, request.Column);
        
        if (!moveSuccess) return BadRequest("Invalid move");
        
        return Ok(session.AcceptResponse());
    }
}

public class MoveRequest
{
    public string SessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}