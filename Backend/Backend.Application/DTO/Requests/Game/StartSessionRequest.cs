using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class StartSessionRequest : IValidatableRequest
{
    public string GameMode { get; set; } = string.Empty;
    public string BotMode { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;
        
        bool isGameModeValid = !string.IsNullOrWhiteSpace(GameMode);
        bool isBotModeValid = !string.IsNullOrWhiteSpace(BotMode);

        bool isValid = isGameModeValid && isBotModeValid;
        
        if (!isGameModeValid) message += "Game Mode is required \n";
        if (!isBotModeValid) message += "Bot Mode is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}