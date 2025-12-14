using Backend.Application.DTO.Entities.Game;
using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Managers.Interfaces;
using Backend.Application.Services.Interfaces;

namespace Backend.Application.Services;

public class GameService : IGameService
{
    private readonly IGameSessionsManager _manager;

    public GameService(IGameSessionsManager manager)
    {
        _manager = manager;
    }

    public async Task<GameResponse> CheckSessionAsync(CheckSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        try
        {
            var session = await _manager.GetSessionAsync(request.SessionId);
            if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");
            return GameResponse.Success("Session was found");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<GameResponse> StartSessionAsync(StartSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);
        
        try
        {
            string sessionId = Guid.NewGuid().ToString();
            
            var session = await _manager.CreateSessionAsync(sessionId, request.GameMode, request.BotMode);
            return session is null
                ? GameResponse.Fail(GameStatusCode.SessionAlreadyExists, "Session already exists")
                : GameResponse.Success(new { sessionId }, "Session was created");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<GameResponse> MakeMoveAsync(MakeMoveRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        try
        {
            var session = await _manager.GetSessionAsync(request.SessionId);
            if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

            bool moveSuccess = session.MakeMove(request.Row, request.Column);
            if (!moveSuccess) return GameResponse.Fail(GameStatusCode.InvalidMove, "Invalid Move");

            await _manager.SetSessionAsync(request.SessionId, session);
            return GameResponse.Success("Successful move");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<GameResponse> GetGameStateAsync(GetBoardStateRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        try
        {
            var session = await _manager.GetSessionAsync(request.SessionId);
            if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

            return GameResponse.Success(new GameStateDto(session), "Current state of the game");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<GameResponse> ResetSessionAsync(ResetSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        try
        {
            bool success = await _manager.ResetSessionAsync(request.SessionId, request.GameMode, request.BotMode);
            if (!success) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

            return GameResponse.Success("Session was reset");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<GameResponse> EndSessionAsync(EndSessionRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return GameResponse.Fail(GameStatusCode.IncorrectData, result.Message);

        try
        {
            bool success = await _manager.RemoveSessionAsync(request.SessionId);
            if (!success) return GameResponse.Fail(GameStatusCode.SessionResetFailed, "Session reset failed");

            return GameResponse.Success("Session was ended");
        }
        catch (Exception)
        {
            return GameResponse.Fail(GameStatusCode.UnknownError, "Internal server error");
        }
    }
}