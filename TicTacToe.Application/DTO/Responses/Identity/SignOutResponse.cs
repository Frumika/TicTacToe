using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class SignOutResponse : BaseResponse<IdentityStatus>
{
    public static SignOutResponse Success(string? message = null)
    {
        return new SignOutResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success
        };
    }
    
    public static SignOutResponse Fail(IdentityStatus code, string? message = null)
    {
        return new SignOutResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}