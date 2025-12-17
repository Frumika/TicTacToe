using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class SignUpResponse : BaseResponse<IdentityStatusCode>
{
    public static SignUpResponse Success(string? message = null)
    {
        return new SignUpResponse 
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success
        };
    }
    
    public static SignUpResponse Fail(IdentityStatusCode code, string? message = null)
    {
        return new SignUpResponse 
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}