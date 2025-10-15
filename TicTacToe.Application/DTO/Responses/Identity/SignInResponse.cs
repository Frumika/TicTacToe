using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class SignInResponse : BaseResponse<IdentityStatus>
{
    public string? SessionId { get; set; } = string.Empty;

    public static SignInResponse Success(string sessionId, string? message = null)
    {
        return new SignInResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success,
            SessionId = sessionId
        };
    }

    public static SignInResponse Fail(IdentityStatus code, string? message = null)
    {
        return new SignInResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            SessionId = null
        };
    }
}