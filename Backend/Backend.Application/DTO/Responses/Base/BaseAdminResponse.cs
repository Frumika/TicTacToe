using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseAdminResponse<TResponse>
    : BaseResponse<AdminStatusCode, TResponse>
    where TResponse : BaseAdminResponse<TResponse>, new()
{
    public new static TResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(AdminStatusCode.Success, data, message);
    }

    public new static TResponse Success(string? message = null)
    {
        return CreateSuccess(AdminStatusCode.Success, message);
    }
}