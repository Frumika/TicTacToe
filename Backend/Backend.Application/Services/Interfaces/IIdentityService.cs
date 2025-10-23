using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;

namespace Backend.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<UpdateDataResponse> UpdateUserDataAsync(UpdateDataRequest request);
    Task<UsersStatisticsResponse> GetUsersStatisticsAsync(GetUsersStatisticsRequest request);
    Task<GetUserDataResponse> GetUserDataAsync(GetUserDataRequest request);
    Task<SignInResponse> SignInUserAsync(IdentityRequest request);
    Task<SignUpResponse> SignUpUserAsync(IdentityRequest request);
    Task<SignOutResponse> SignOutUserAsync(SignOutRequest request);
}