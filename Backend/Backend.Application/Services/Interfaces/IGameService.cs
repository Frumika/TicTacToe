using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;

namespace Backend.Application.Services.Interfaces;

public interface IGameService
{
    Task<CheckSessionResponse> CheckSessionAsync(CheckSessionRequest request);
    Task<StartSessionResponse> StartSessionAsync(StartSessionRequest request);
    Task<MakeMoveResponse> MakeMoveAsync(MakeMoveRequest request);
    Task<GetBoardStateResponse> GetBoardStateAsync(GetBoardStateRequest request);
    Task<ResetSessionResponse> ResetSessionAsync(ResetSessionRequest request);
    Task<EndSessionResponse> EndSessionAsync(EndSessionRequest request);
}