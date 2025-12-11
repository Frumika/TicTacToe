using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;

namespace Backend.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<IdentityResponse> GetUserByLoginAsync(string login);
    Task<IdentityResponse> GetUsersListAsync(GetUsersListRequest request);
    Task<IdentityResponse> ChangeUserDataAsync(ChangeUserDataRequest request);
    Task<IdentityResponse> UpdateUserStatsAsync(UpdateUserStatsRequest request);
    Task<IdentityResponse> GetUsersStatsAsync(GetUsersStatisticsRequest request);
    Task<IdentityResponse> GetUserDataAsyncBySessionId(GetUserDataRequest request);
    Task<IdentityResponse> SignInUserAsync(IdentityRequest request);
    Task<IdentityResponse> SignUpUserAsync(IdentityRequest request);
    Task<IdentityResponse> SignOutUserAsync(SignOutRequest request);
}