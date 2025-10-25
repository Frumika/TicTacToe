using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;
using Backend.Application.Services.Interfaces;

namespace Backend.Application.Services;

public class GameService : IGameService
{
    public Task<CheckSessionResponse> CheckSessionAsync(CheckSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<StartSessionResponse> StartSessionAsync(StartSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<MakeMoveResponse> MakeMoveAsync(MakeMoveRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GetBoardStateResponse> GetBoardStateAsync(GetBoardStateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ResetSessionResponse> ResetSessionAsync(ResetSessionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<EndSessionResponse> EndSessionAsync(EndSessionRequest request)
    {
        throw new NotImplementedException();
    }
}