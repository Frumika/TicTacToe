using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }


    [HttpPost("check")]
    public async Task<IActionResult> CheckSession(CheckSessionRequest request)
    {
        var response = await _gameService.CheckSessionAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartSession([FromBody] StartSessionRequest request)
    {
        var response = await _gameService.StartSessionAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("move")]
    public async Task<IActionResult> MakeMove([FromBody] MakeMoveRequest request)
    {
        var response = await _gameService.MakeMoveAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("state")]
    public async Task<IActionResult> GetGameState(GetBoardStateRequest request)
    {
        var response = await _gameService.GetGameStateAsync(request);
        
        Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
        
        return ToHttpResponse(response);
    }

    [HttpPost("reset")]
    public async Task<IActionResult> ResetSession(ResetSessionRequest request)
    {
        var response = await _gameService.ResetSessionAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPost("end")]
    public async Task<IActionResult> EndSession(EndSessionRequest request)
    {
        var response = await _gameService.EndSessionAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(GameResponse response)
    {
        return response.Code switch
        {
            GameStatusCode.Success => Ok(response),
            GameStatusCode.IncorrectData => BadRequest(response),
            GameStatusCode.SessionNotFound => NotFound(response),
            GameStatusCode.SessionAlreadyExists => Conflict(response),
            GameStatusCode.InvalidMove => BadRequest(response),
            GameStatusCode.SessionResetFailed => StatusCode(500, response),
            GameStatusCode.UnknownError => StatusCode(500, response),
            _ => BadRequest(response)
        };
    }
}