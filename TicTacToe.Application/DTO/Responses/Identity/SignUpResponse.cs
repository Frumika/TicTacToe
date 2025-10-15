using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class SignUpResponse : BaseResponse<IdentityStatus>
{
    public static SignUpResponse Success(string? message = null)
    {
        return new SignUpResponse 
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success
        };
    }
    
    public static SignUpResponse Fail(IdentityStatus code, string? message = null)
    {
        return new SignUpResponse 
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}