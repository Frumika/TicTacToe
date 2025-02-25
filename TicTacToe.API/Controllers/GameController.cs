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

    [HttpPost("start")]
    public IActionResult StartSession([FromBody] string sessionId)
    {
        var session = _gameSessionsService.GetOrCreateSession(sessionId);
        return Ok(session.AcceptResponse());
    }


    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        if (string.IsNullOrEmpty(request.SessionId)) return BadRequest(new { error = "Empty sessionId" });

        Console.WriteLine($"Request: id = {request.SessionId} ({request.Row} : {request.Column})");

        var session = _gameSessionsService.GetOrCreateSession(request.SessionId);
        bool moveSuccess = session.SendRequest(request.Row, request.Column);

        if (!moveSuccess) return BadRequest(new { error = "Invalid move" });

        return Ok(session.AcceptResponse());
    }


    [HttpPost("state")]
    public IActionResult GetBoardState([FromBody] string sessionId)
    {
        var session = _gameSessionsService.GetSession(sessionId);

        if (session is null) return NotFound(new { error = "Session not found" });

        return Ok(session.AcceptResponse());
    }

    [HttpPost("reset")]
    public IActionResult ResetSession([FromBody] string sessionId)
    {
        bool success = _gameSessionsService.ResetSession(sessionId);

        if (!success) return NotFound(new { error = "Session not found." });

        return Ok(new { success = "Session reset successfully." });
    }


    [HttpDelete("end")]
    public IActionResult EndSession([FromQuery] string sessionId)
    {
        bool success = _gameSessionsService.RemoveSession(sessionId);

        if (!success) return NotFound(new { error = "Session not found." });

        return NoContent(); // Удалено успешно
    }
}

public class MoveRequest
{
    public string? SessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}