using Backend.Application.DTO.Requests.Admin;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("get/{login}")]
    public async Task<IActionResult> GetUserByLogin([FromRoute] string login)
    {
        var response = await _adminService.GetUserByLoginAsync(login);
        return ToHttpRequest(response);
    }

    [HttpPost("get/list")]
    public async Task<IActionResult> GetUsersList([FromBody] GetUsersListRequest request)
    {
        var response = await _adminService.GetUsersListAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataRequest request)
    {
        var response = await _adminService.UpdateUserDataAsync(request);
        return ToHttpRequest(response);
    }

    [HttpDelete("delete/{login}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string login)
    {
        var response = await _adminService.DeleteUserAsync(login);
        return ToHttpRequest(response);
    }
    
    
    private IActionResult ToHttpRequest(AdminResponse response)
    {
        return response.Code switch
        {
            AdminStatusCode.Success => Ok(response),
            AdminStatusCode.InvalidLogin => BadRequest(response),
            AdminStatusCode.IncorrectData => BadRequest(response),
            AdminStatusCode.UserNotFound => NotFound(response),
            AdminStatusCode.UserAlreadyExists => Conflict(response),
            AdminStatusCode.UnknownError => StatusCode(500, response),
            _ => BadRequest(response)
        };
    }
}