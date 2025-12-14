using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> UpdateUserStatsAsync(UpdateUserStatsRequest request);
    Task<UserResponse> GetUsersStatsAsync(GetUsersStatisticsRequest request);
    Task<UserResponse> GetUserDataAsyncBySessionId(GetUserDataRequest request);
    Task<UserResponse> SignInUserAsync(UserRequest request);
    Task<UserResponse> SignUpUserAsync(UserRequest request);
    Task<UserResponse> LogoutUserSessionAsync(LogoutUserSessionRequest request);
    Task<UserResponse> LogoutAllUserSessionsAsync(int userId);
}