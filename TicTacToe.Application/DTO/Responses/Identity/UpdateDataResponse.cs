using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class UpdateDataResponse : BaseResponse<IdentityStatusCode>
{
    public static UpdateDataResponse Success(string? message = null)
    {
        return new UpdateDataResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success
        };
    }

    public static UpdateDataResponse Fail(IdentityStatusCode code, string? message = null)
    {
        return new UpdateDataResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
        };
    }
}