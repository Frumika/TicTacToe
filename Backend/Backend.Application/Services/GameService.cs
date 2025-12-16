using Backend.Application.DTO.Entities.Game;
using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Managers.Interfaces;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Context;
using Backend.Domain.Enums;
using Backend.Domain.Models.App;
using Backend.Domain.Models.Game;

namespace Backend.Application.Services;

public class GameService : IGameService
{
    private readonly AppDbContext _appDbContext;
    private readonly IGameSessionsManager _manager;

    public GameService(AppDbContext appDbContext, IGameSessionsManager manager)
    {
        _appDbContext = appDbContext;
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

            var session = await _manager
                .CreateSessionAsync(request.UserId, sessionId, request.GameMode, request.BotMode);

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
            Session? session = await _manager.GetSessionAsync(request.SessionId);
            if (session is null) return GameResponse.Fail(GameStatusCode.SessionNotFound, "Session not found");

            FieldItem currentItem = session.CurrentItem;

            bool moveSuccess = session.MakeMove(request.Row, request.Column);
            if (!moveSuccess) return GameResponse.Fail(GameStatusCode.InvalidMove, "Invalid Move");

            if (session.UserId is not null)
            {
                var gameMoveState = new GameMove
                {
                    UserId = session.UserId.Value,
                    GameSessionId = request.SessionId,
                    ResetCount = session.ResetCount,
                    PlayerItem = currentItem,
                    Row = request.Row,
                    Column = request.Column,
                    CreatedAt = DateTime.UtcNow
                };

                _appDbContext.GameMoves.Add(gameMoveState);
                await _appDbContext.SaveChangesAsync();
            }

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
            bool success = await _manager
                .ResetSessionAsync(request.UserId, request.SessionId, request.GameMode, request.BotMode);

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