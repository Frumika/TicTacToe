using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseIdentityResponse<TResponse>
    : BaseResponse<UserStatusCode, TResponse>
    where TResponse : BaseIdentityResponse<TResponse>, new()
{
    public new static TResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(UserStatusCode.Success, data, message);
    }

    public new static TResponse Success(string? message = null)
    {
        return CreateSuccess(UserStatusCode.Success, message);
    }
}