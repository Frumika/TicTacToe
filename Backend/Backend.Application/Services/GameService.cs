using Backend.Application.DTO.Entities.Game;
using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;
using Backend.Application.Enums;
using Backend.Application.Managers;
using Backend.Application.Services.Interfaces;

namespace Backend.Application.Services;

public class GameService : IGameService
{
    private readonly GameSessionsManager _gameSessionsManager;

    public GameService(GameSessionsManager gameSessionsManager)
    {
        _gameSessionsManager = gameSessionsManager;
    }

    public async Task<GameResponse> CheckSessionAsync(CheckSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        var session = _gameSessionsManager.GetSession(request.SessionId);
        if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

        return GameResponse.Success(null, "Session was found");
    }

    public async Task<GameResponse> StartSessionAsync(StartSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        var session = _gameSessionsManager.CreateSession(request.SessionId, request.GameMode, request.BotMode);
        if (!session) return GameResponse.Fail(GameStatusCode.SessionAlreadyExists, "Session already exists");

        return GameResponse.Success(null, "Session was created");
    }

    public async Task<GameResponse> MakeMoveAsync(MakeMoveRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        var session = _gameSessionsManager.GetSession(request.SessionId);
        if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

        bool moveSuccess = session.MakeMove(request.Row, request.Column);
        if (!moveSuccess) return GameResponse.Fail(GameStatusCode.InvalidMove, "Invalid Move");

        return GameResponse.Success(null, "Successful move");
    }

    public async Task<GameResponse> GetGameStateAsync(GetBoardStateRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        var session = _gameSessionsManager.GetSession(request.SessionId);
        if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

        return GameResponse.Success(new GameStateDto(session), "Current state of the game");
    }

    public async Task<GameResponse> ResetSessionAsync(ResetSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        bool success = _gameSessionsManager.ResetSession(request.SessionId, request.GameMode, request.BotMode);
        if (!success) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

        return GameResponse.Success(null, "Session was reset");
    }

    public async Task<GameResponse> EndSessionAsync(EndSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        bool success = _gameSessionsManager.RemoveSession(request.SessionId);
        if (!success) return GameResponse.Fail(GameStatusCode.SessionResetFailed, "Session reset failed");

        return GameResponse.Success(null, "Session was ended");
    }
}