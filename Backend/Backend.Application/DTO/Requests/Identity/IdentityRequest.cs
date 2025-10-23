using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Identity;

public class IdentityRequest : IValidatableRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;

        bool isLoginValid = !string.IsNullOrWhiteSpace(Login);
        bool isPasswordValid = !string.IsNullOrWhiteSpace(Password);
        bool isValid = isLoginValid && isPasswordValid;

        if (!isLoginValid) message += "Login is required";
        if (!isPasswordValid) message += "Password is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}