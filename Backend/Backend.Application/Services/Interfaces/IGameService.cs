using Backend.Application.DTO.Requests.Game;
using Backend.Application.DTO.Responses.Game;

namespace Backend.Application.Services.Interfaces;

public interface IGameService
{
    Task<GameResponse> CheckSessionAsync(CheckSessionRequest request);
    Task<GameResponse> StartSessionAsync(StartSessionRequest request);
    Task<GameResponse> MakeMoveAsync(MakeMoveRequest request);
    Task<GameResponse> GetBoardStateAsync(GetBoardStateRequest request);
    Task<GameResponse> ResetSessionAsync(ResetSessionRequest request);
    Task<GameResponse> EndSessionAsync(EndSessionRequest request);
}