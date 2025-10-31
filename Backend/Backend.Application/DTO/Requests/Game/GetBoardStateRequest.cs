using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class GetBoardStateRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;


    public ValidationResult Validate()
    {
        string? message = null;

        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);

        if (!isSessionIdValid) message += "Session Id is required";

        return isSessionIdValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}