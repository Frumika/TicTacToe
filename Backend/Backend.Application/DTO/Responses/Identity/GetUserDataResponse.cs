using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Identity;

public class GetUserDataResponse : BaseResponse<IdentityStatusCode>
{
    public UserDto? User { get; set; }

    public static GetUserDataResponse Success(UserDto user, string? message = null)
    {
        return new GetUserDataResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success,
            User = user
        };
    }

    public static GetUserDataResponse Fail(IdentityStatusCode code, string? message = null)
    {
        return new GetUserDataResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            User = null
        };
    }
}