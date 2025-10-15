using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class UpdateDataResponse : BaseResponse<IdentityStatus>
{
    public static UpdateDataResponse Success(string? message = null)
    {
        return new UpdateDataResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success
        };
    }

    public static UpdateDataResponse Fail(IdentityStatus code, string? message = null)
    {
        return new UpdateDataResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
        };
    }
}