using Backend.Application.DTO.Requests.Admin;
using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses;

namespace Backend.Application.Services.Interfaces;

public interface IAdminService
{
    Task<AdminResponse> GetUserByLoginAsync(string login);
    Task<AdminResponse> GetUsersListAsync(GetUsersListRequest request);
    Task<AdminResponse> UpdateUserDataAsync(UpdateUserDataRequest request);
    Task<AdminResponse> DeleteUserAsync(string login);
}