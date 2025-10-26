using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseIdentityResponse<TResponse>
    : BaseResponse<IdentityStatusCode, TResponse>
    where TResponse : BaseIdentityResponse<TResponse>, new()
{
    public new static TResponse Success(BaseDto? data = null, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success,
            Data = data
        };
    }
}