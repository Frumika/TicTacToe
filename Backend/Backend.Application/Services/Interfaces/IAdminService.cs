using Backend.Application.DTO.Requests.Admin;
using Backend.Application.DTO.Responses;

namespace Backend.Application.Services.Interfaces;

public interface IAdminService
{
    Task<AdminResponse> GetUserByIdAsync(int id);
    Task<AdminResponse> GetUsersListAsync(GetUsersListRequest request);
    Task<AdminResponse> UpdateUserDataAsync(UpdateUserDataRequest request);
    Task<AdminResponse> DeleteUserAsync(int id);
}