using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Identity;

public class SignOutResponse : BaseResponse<IdentityStatusCode>
{
    public static SignOutResponse Success(string? message = null)
    {
        return new SignOutResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success
        };
    }
    
    public static SignOutResponse Fail(IdentityStatusCode code, string? message = null)
    {
        return new SignOutResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}