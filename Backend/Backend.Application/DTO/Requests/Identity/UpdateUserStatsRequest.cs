using Backend.Application.DTO.Requests.Base;
using Backend.Application.Enums;


namespace Backend.Application.DTO.Requests.Identity;

public class UpdateUserStatsRequest : IValidatableRequest
{
    public string Login { get; set; } = string.Empty;
    public EndGameType Type { get; set; }

    public ValidationResult Validate()
    {
        string? message = null;
        bool isLoginValid = !string.IsNullOrWhiteSpace(Login);
        bool isTypeValid = Type != EndGameType.Undefined;

        if (!isLoginValid) message += "Login must be required";
        if (!isTypeValid) message += "Type is undefined";
        
        bool isValid = isLoginValid && isTypeValid;

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}