using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;
using TicTacToe.API.Requests;


[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private GameSessionsService _gameSessionsService;

    public GameController(GameSessionsService gameSessionsService)
    {
        _gameSessionsService = gameSessionsService;
    }


    [HttpPost("check")]
    public IActionResult CheckSession([FromBody] string sessionId)
    {
        var session = _gameSessionsService.GetSession(sessionId);
        if (session is null) return NotFound(new { message = "Session not found" });
        return Ok(new { message = "Session was found" });
    }

    [HttpPost("start")]
    public IActionResult StartSession([FromBody] GameInfoRequest request)
    {
        var session = _gameSessionsService.CreateSession(request.GameSessionId, request.GameMode, request.BotMode);
        if (!session) return BadRequest(new { message = "Session not create" });

        return Ok(new { message = "Session was created" });
    }

    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest request)
    {
        if (string.IsNullOrEmpty(request.GameSessionId)) return BadRequest(new { message = "Empty sessionId" });

        var session = _gameSessionsService.GetSession(request.GameSessionId);
        if (session is null) return BadRequest(new { message = "Session not found" });

        bool moveSuccess = session.MakeMove(request.Row, request.Column);
        if (!moveSuccess) return BadRequest(new { message = "Invalid move" });

        return Ok(new { message = "Successful move" });
    }

    [HttpPost("state")]
    public IActionResult GetBoardState([FromBody] string sessionId)
    {
        var session = _gameSessionsService.GetSession(sessionId);
        if (session is null) return NotFound(new { message = "Session not found" });

        return Ok(session.GameState());
    }

    [HttpPost("reset")]
    public IActionResult ResetSession([FromBody] GameInfoRequest request)
    {
        bool success = _gameSessionsService.ResetSession(request.GameSessionId, request.GameMode, request.BotMode);

        if (!success) return NotFound(new { message = "Session not found." });

        return Ok(new { message = "Session reset successfully." });
    }


    [HttpPost("end")]
    public IActionResult EndSession([FromQuery] string sessionId)
    {
        bool success = _gameSessionsService.RemoveSession(sessionId);

        if (!success) return NotFound(new { message = "Session not found." });

        return NoContent(); // Удалено успешно
    }
}