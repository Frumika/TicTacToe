using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseGameResponse<TResponse>
    : BaseResponse<GameStatusCode, TResponse>
    where TResponse : BaseGameResponse<TResponse>, new()
{
    public new static TResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(GameStatusCode.Success, data, message);
    }

    public new static TResponse Success(string? message = null)
    {
        return CreateSuccess(GameStatusCode.Success, message);
    }
}