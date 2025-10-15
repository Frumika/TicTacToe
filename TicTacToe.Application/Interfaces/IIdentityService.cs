using TicTacToe.Application.DTO.Requests;
using TicTacToe.Application.DTO.Requests.Identity;
using TicTacToe.Application.DTO.Responses;
using TicTacToe.Application.DTO.Responses.Identity;
using TicTacToe.Application.Enums;

namespace TicTacToe.Application.Interfaces;

public interface IIdentityService
{
    Task<UpdateDataResponse> UpdateUserDataAsync(UpdateDataRequest request);
    Task<UsersStatisticsResponse> GetUsersStatisticsAsync(UsersStatisticsRequest request);
    Task<GetUserDataResponse> GetUserDataAsync(string sessionId);
    Task<SignInResponse> SignInUserAsync(IdentityRequest request);
    Task<SignUpResponse> SignUpUserAsync(IdentityRequest request);
    Task<SignOutResponse> SignOutUserAsync(string sessionId);
}