using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;

namespace Backend.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<IdentityResponse> UpdateUserStatsAsync(UpdateUserStatsRequest request);
    Task<IdentityResponse> GetUsersStatisticsAsync(GetUsersStatisticsRequest request);
    Task<IdentityResponse> GetUserDataAsync(GetUserDataRequest request);
    Task<IdentityResponse> SignInUserAsync(IdentityRequest request);
    Task<IdentityResponse> SignUpUserAsync(IdentityRequest request);
    Task<IdentityResponse> SignOutUserAsync(SignOutRequest request);
}