using Backend.Application.DTO.Entities.Identity;
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
    private readonly UsersDbContext _usersDbContext;

    public AdminService(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<AdminResponse> GetUserByLoginAsync(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return AdminResponse.Fail(AdminStatusCode.InvalidLogin, "Login cannot be null or empty");

        try
        {
            User? user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Login == login);

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
            var users = await _usersDbContext.Users.AsNoTracking()
                .OrderBy(user => user.Login)
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
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Login == request.OldLogin);
            if (user is null) return AdminResponse.Fail(AdminStatusCode.UserNotFound, "User not found");

            var existing = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Login == request.NewLogin);
            if (existing is not null && request.NewLogin != user.Login)
                return AdminResponse
                    .Fail(AdminStatusCode.UserAlreadyExists, "User with this login already exists");

            user.Login = request.NewLogin;
            user.Wins = request.Wins;
            user.Losses = request.Losses;
            user.Draws = request.Draws;
            user.Matches = request.Wins + request.Losses + request.Draws;

            _usersDbContext.Users.Update(user);
            await _usersDbContext.SaveChangesAsync();

            return AdminResponse.Success(new UserDto(user), "The user data was updated");
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }


    public async Task<AdminResponse> DeleteUserAsync(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return AdminResponse.Fail(AdminStatusCode.InvalidLogin, "Login cannot be null or empty");

        try
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user is null) return AdminResponse.Fail(AdminStatusCode.UserNotFound, "User not found");

            _usersDbContext.Users.Remove(user);
            await _usersDbContext.SaveChangesAsync();

            return AdminResponse.Success("The user was deleted");
        }
        catch (Exception)
        {
            return AdminResponse.Fail(AdminStatusCode.UnknownError, "Internal server error");
        }
    }
}