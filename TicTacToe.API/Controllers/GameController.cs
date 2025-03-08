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
        Console.WriteLine($"|> Contorller seID: {request.GameSessionId}");

        if (string.IsNullOrEmpty(request.GameSessionId)) return BadRequest(new { error = "Empty sessionId" });
        if (string.IsNullOrEmpty(request.GameMode)) return BadRequest(new { error = "Empty GameMode" });

        var session = _gameSessionsService.GetSession(request.GameSessionId);
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
        if (string.IsNullOrEmpty(request.GameSessionId)) return BadRequest(new { message = "Empty sessionId" });

        var session = _gameSessionsService.GetOrCreateSession(request.GameSessionId);
        bool moveSuccess = session.SendRequest(request.Row, request.Column);

        if (!moveSuccess) return BadRequest(new { message = "Invalid move" });

        return Ok(new { message = "Successful move" });
    }


    [HttpPost("state")]
    public IActionResult GetBoardState([FromBody] string sessionId)
    {
        var session = _gameSessionsService.GetSession(sessionId);
        if (session is null) return NotFound(new { message = "Session not found" });

        return Ok(session.AcceptResponse());
    }

    [HttpPost("reset")]
    public IActionResult ResetSession([FromBody] string sessionId)
    {
        Console.WriteLine("|> Controller: reset");

        bool success = _gameSessionsService.ResetSession(sessionId);

        if (!success) return NotFound(new { message = "Session not found." });

        return Ok(new { message = "Session reset successfully." });
    }


    [HttpPost("end")]
    public IActionResult EndSession([FromQuery] string sessionId)
    {
        Console.WriteLine("|> Controller: end");

        bool success = _gameSessionsService.RemoveSession(sessionId);

        if (!success) return NotFound(new { message = "Session not found." });

        return NoContent(); // Удалено успешно
    }
}

public class MoveRequest
{
    public string? GameSessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}

public class SettingsRequest
{
    public string? GameSessionId { get; set; }
    public string? GameMode { get; set; }
}