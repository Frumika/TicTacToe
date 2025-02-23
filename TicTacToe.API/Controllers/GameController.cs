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

        var game = _gameSessionsService.GetOrCreateSession(request.SessionId);
        
        game.SendRequest(request.Row, request.Column);

        return Ok(new { success = true });
    }
}

public class MoveRequest
{
    public string SessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}