using Backend.Application.DTO.Entities.User;
using Backend.Application.DTO.Requests.Admin;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;
using Backend.DataAccess.Postgres.Context;
using Backend.Domain.Models.App;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _appDbContext;

    public AdminService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<AdminResponse> GetUserByIdAsync(int id)
    {
        if (id <= 0)
            return AdminResponse.Fail(AdminStatusCode.IncorrectData, "User ID must be greater than 0");

        try
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user is null
                ? AdminResponse.Fail(AdminStatusCode.UserNotFound, "User not found")
                : AdminResponse.Success(new UserDto(user));
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AdminResponse> GetUsersListAsync(GetUsersListRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return AdminResponse.Fail(AdminStatusCode.IncorrectData, result.Message);

        try
        {
            var users = await _appDbContext.Users.AsNoTracking()
                .OrderBy(user => user.Id)
                .Skip(request.SkipModifier * request.UsersCount)
                .Take(request.UsersCount + 1)
                .Select(user => new UserDto(user))
                .ToListAsync();

            bool isLastPage = users.Count <= request.UsersCount;
            if (!isLastPage) users.RemoveAt(users.Count - 1);

            return AdminResponse.Success(new UsersListDto { Users = users, IsLastPage = isLastPage });
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AdminResponse> UpdateUserDataAsync(UpdateUserDataRequest request)
    {
        var result = request.Validate();
        if (!result.IsValid) return AdminResponse.Fail(AdminStatusCode.IncorrectData, result.Message);

        try
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);
            if (user is null) return AdminResponse.Fail(AdminStatusCode.UserNotFound, "User not found");

            user.Login = request.UserLogin;
            user.Wins = request.Wins;
            user.Losses = request.Losses;
            user.Draws = request.Draws;
            user.Matches = request.Wins + request.Losses + request.Draws;

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            return AdminResponse.Success(new UserDto(user), "The user data was updated");
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }


    public async Task<AdminResponse> DeleteUserAsync(int id)
    {
        if (id <= 0)
            return AdminResponse.Fail(AdminStatusCode.IncorrectData, "Login cannot be null or empty");

        try
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return AdminResponse.Fail(AdminStatusCode.UserNotFound, "User not found");

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return AdminResponse.Success("The user was deleted");
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }
}