using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseIdentityResponse<TResponse>
    : BaseResponse<IdentityStatusCode, TResponse>
    where TResponse : BaseIdentityResponse<TResponse>, new()
{
    public new static TResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(IdentityStatusCode.Success, data, message);
    }

    public new static TResponse Success(string? message = null)
    {
        return CreateSuccess(IdentityStatusCode.Success, message);
    }
}