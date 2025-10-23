namespace Backend.Application.DTO.Responses;

public abstract class BaseResponse<TCode> where TCode : Enum
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public TCode? Code { get; set; }
}