using Microsoft.AspNetCore.Mvc;
using Backend.Application.DTO.Requests.Identity;
using Backend.Application.DTO.Responses.Identity;
using Backend.Application.Enums;
using Backend.Application.Services.Interfaces;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateUserStats([FromBody] UpdateUserStatsRequest request)
    {
        var response = await _identityService.UpdateUserStatsAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("statistics")]
    public async Task<IActionResult> GetUsersStatistics([FromBody] GetUsersStatisticsRequest request)
    {
        var response = await _identityService.GetUsersStatisticsAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("info")]
    public async Task<IActionResult> GetUserData([FromBody] GetUserDataRequest request)
    {
        var response = await _identityService.GetUserDataAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] IdentityRequest request)
    {
        var response = await _identityService.SignInUserAsync(request);
        return ToHttpRequest(response);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser([FromBody] IdentityRequest request)
    {
        var response = await _identityService.SignUpUserAsync(request);
        return ToHttpRequest(response);
    }

    [HttpDelete("signout")]
    public async Task<IActionResult> SignOutUser([FromBody] SignOutRequest request)
    {
        var response = await _identityService.SignOutUserAsync(request);
        return ToHttpRequest(response);
    }

    private IActionResult ToHttpRequest(IdentityResponse response)
    {
        return response.Code switch
        {
            IdentityStatusCode.Success => Ok(response),
            IdentityStatusCode.InvalidLogin => BadRequest(response),
            IdentityStatusCode.InvalidPassword => Unauthorized(response),
            IdentityStatusCode.IncorrectData => BadRequest(response),
            IdentityStatusCode.UserAlreadyExists => Conflict(response),
            IdentityStatusCode.UserNotFound => NotFound(response),
            IdentityStatusCode.UnknownError => StatusCode(500, response),
            _ => BadRequest(response)
        };
    }
}