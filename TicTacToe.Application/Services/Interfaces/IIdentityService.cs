using TicTacToe.Application.DTO.Requests.Identity;
using TicTacToe.Application.DTO.Responses.Identity;

namespace TicTacToe.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<UpdateDataResponse> UpdateUserDataAsync(UpdateDataRequest request);
    Task<UsersStatisticsResponse> GetUsersStatisticsAsync(GetUsersStatisticsRequest request);
    Task<GetUserDataResponse> GetUserDataAsync(GetUserDataRequest request);
    Task<SignInResponse> SignInUserAsync(IdentityRequest request);
    Task<SignUpResponse> SignUpUserAsync(IdentityRequest request);
    Task<SignOutResponse> SignOutUserAsync(SignOutRequest request);
}