using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseGameResponse<TResponse>
    : BaseResponse<GameStatusCode, TResponse>
    where TResponse : BaseGameResponse<TResponse>, new()
{
    public new static TResponse Success(BaseDto? data = null, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Code = GameStatusCode.Success,
            Data = data
        };
    }
}