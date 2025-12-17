using TicTacToe.Application.DTO.Requests.Base;

namespace TicTacToe.Application.DTO.Requests.Identity;

public class UpdateDataRequest : IValidatableRequest
{
    public string Login { get; set; } = string.Empty;
    public bool IsWin { get; set; } = false;

    public ValidationResult Validate()
    {
        string? message = null;
        bool isLoginValid = !string.IsNullOrWhiteSpace(Login);

        if (!isLoginValid) message += "Login is required";

        return isLoginValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}