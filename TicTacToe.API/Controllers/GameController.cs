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

    [HttpPost("settings")]
    public IActionResult ConfirmSettings([FromBody] SettingsRequest request)
    {
        Console.WriteLine("|> Controller: settings");
        Console.WriteLine($"|> Contorller seID: {request.SessionId}");
        
        if (string.IsNullOrEmpty(request.SessionId)) return BadRequest(new { error = "Empty sessionId" });
        if (string.IsNullOrEmpty(request.GameMode)) return BadRequest(new { error = "Empty GameMode" });

        var session = _gameSessionsService.GetSession(request.SessionId);
        if (session is null) return NotFound(new { error = "Session not found" });

        session?.ApplySetting(request.GameMode);
        return Ok(new { success = "Settings confirmed." });
    }

    [HttpPost("start")]
    public IActionResult StartSession([FromBody] string sessionId)
    {
        Console.WriteLine("|> Controller: start");
        Console.WriteLine($"|> Contorller sID: {sessionId}");
        
        var session = _gameSessionsService.GetOrCreateSession(sessionId);
        return Ok(session.AcceptResponse());
    }


    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        Console.WriteLine("|> Controller: move");
        
        if (string.IsNullOrEmpty(request.SessionId)) return BadRequest(new { error = "Empty sessionId" });

        var session = _gameSessionsService.GetOrCreateSession(request.SessionId);
        bool moveSuccess = session.SendRequest(request.Row, request.Column);

        if (!moveSuccess) return BadRequest(new { error = "Invalid move" });

        return Ok(session.AcceptResponse());
    }


    [HttpPost("state")]
    public IActionResult GetBoardState([FromBody] string sessionId)
    {
        Console.WriteLine("|> Controller: state");
        
        var session = _gameSessionsService.GetSession(sessionId);

        if (session is null) return NotFound(new { error = "Session not found" });

        return Ok(session.AcceptResponse());
    }

    [HttpPost("reset")]
    public IActionResult ResetSession([FromBody] string sessionId)
    {
        Console.WriteLine("|> Controller: reset");
        
        bool success = _gameSessionsService.ResetSession(sessionId);

        if (!success) return NotFound(new { error = "Session not found." });

        return Ok(new { success = "Session reset successfully." });
    }


    [HttpPost("end")]
    public IActionResult EndSession([FromQuery] string sessionId)
    {
        Console.WriteLine("|> Controller: end");
        
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

public class SettingsRequest
{
    public string? SessionId { get; set; }
    public string? GameMode { get; set; }
}