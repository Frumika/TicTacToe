using System.ComponentModel.DataAnnotations;
using TicTacToe.Application.DTO.Requests.Base;
using ValidationResult = TicTacToe.Application.DTO.Requests.Base.ValidationResult;

namespace TicTacToe.Application.DTO.Requests.Identity;

public class IdentityRequest : IValidatableRequest
{
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
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