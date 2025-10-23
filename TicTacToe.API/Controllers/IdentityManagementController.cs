using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.DTO.Requests.Identity;
using TicTacToe.Application.DTO.Responses;
using TicTacToe.Application.Enums;
using TicTacToe.Application.Services.Interfaces;


namespace TicTacToe.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityManagementController : ControllerBase
{
    private readonly IIdentityService _identityService;
    
    public IdentityManagementController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateDataRequest request)
    {
        var response = await _identityService.UpdateUserDataAsync(request);
        return ToHttpResult(response);
    }

    [HttpPost("statistics")]
    public async Task<IActionResult> GetUsersStatistics([FromBody] GetUsersStatisticsRequest request)
    {
        var response = await _identityService.GetUsersStatisticsAsync(request);
        return ToHttpResult(response);
    }

    [HttpPost("info")]
    public async Task<IActionResult> GetUserData([FromBody] GetUserDataRequest request)
    {
        var response = await _identityService.GetUserDataAsync(request);
        return ToHttpResult(response);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser([FromBody] IdentityRequest request)
    {
        var response = await _identityService.SignInUserAsync(request);
        return ToHttpResult(response);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser([FromBody] IdentityRequest request)
    {
        var response = await _identityService.SignUpUserAsync(request);
        return ToHttpResult(response);
    }
    
    [HttpPost("signout")]
    public async Task<IActionResult> SignOutUser([FromBody] SignOutRequest request)
    {
        var response = await _identityService.SignOutUserAsync(request);
        return ToHttpResult(response);
    }
    
    private IActionResult ToHttpResult(BaseResponse<IdentityStatusCode> response)
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