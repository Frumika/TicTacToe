using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;
using Backend.Application.Services.Interfaces;

namespace Backend.Application.Services;

public class GameService : IGameService
{
    public Task<GameResponse> CheckSessionAsync(CheckSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GameResponse> StartSessionAsync(StartSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GameResponse> MakeMoveAsync(MakeMoveRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GameResponse> GetBoardStateAsync(GetBoardStateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GameResponse> ResetSessionAsync(ResetSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GameResponse> EndSessionAsync(EndSessionRequest request)
    {
        throw new NotImplementedException();
    }
}