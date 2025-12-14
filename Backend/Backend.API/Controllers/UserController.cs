using Backend.Application.DTO.Requests.User;
using Microsoft.AspNetCore.Mvc;
using Backend.Application.DTO.Responses;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/identity")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPatch("update_stats")]
    public async Task<IActionResult> UpdateUserStats([FromBody] UpdateUserStatsRequest request)
    {
        var response = await _userService.UpdateUserStatsAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("statistics")]
    public async Task<IActionResult> GetUsersStatistics([FromBody] GetUsersStatisticsRequest request)
    {
        var response = await _userService.GetUsersStatsAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("session_info")]
    public async Task<IActionResult> GetUserData([FromBody] GetUserDataRequest request)
    {
        var response = await _userService.GetUserDataAsyncBySessionId(request);
        return ToHttpRequest(response);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] UserRequest request)
    {
        var response = await _userService.SignInUserAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser([FromBody] UserRequest request)
    {
        var response = await _userService.SignUpUserAsync(request);
        return ToHttpRequest(response);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutUserSession([FromBody] LogoutUserSessionRequest request)
    {
        var response = await _userService.LogoutUserSessionAsync(request);
        return ToHttpRequest(response);
    }

    [HttpDelete("logout_all/{userId}")]
    public async Task<IActionResult> LogoutAllUserSessions([FromRoute] int userId)
    {
        var response = await _userService.LogoutAllUserSessionsAsync(userId);
        return ToHttpRequest(response);
    }

    private IActionResult ToHttpRequest(UserResponse response)
    {
        return response.Code switch
        {
            UserStatusCode.Success => Ok(response),
            UserStatusCode.InvalidLogin => BadRequest(response),
            UserStatusCode.InvalidPassword => Unauthorized(response),
            UserStatusCode.IncorrectData => BadRequest(response),
            UserStatusCode.UserAlreadyExists => Conflict(response),
            UserStatusCode.UserNotFound => NotFound(response),
            UserStatusCode.UnknownError => StatusCode(500, response),
            _ => BadRequest(response)
        };
    }
}