using Backend.Application.DTO.Entities;

namespace Backend.Application.DTO.Responses.Base;

public abstract class BaseResponse<TCode, TResponse>
    where TCode : Enum
    where TResponse : BaseResponse<TCode, TResponse>, new()
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public TCode? Code { get; set; }
    public BaseDto? Data { get; set; }

    public static TResponse Success(BaseDto? data = null, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Code = default,
            Data = data
        };
    }

    public static TResponse Fail(TCode code, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Data = null
        };
    }
}