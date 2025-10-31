using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class EndSessionRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;
    
    public ValidationResult Validate()
    {
        string? message = null;
        
        bool isValidSessionId = !string.IsNullOrWhiteSpace(SessionId);
        if (!isValidSessionId) message += "Session id is required";
        
        return isValidSessionId ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}