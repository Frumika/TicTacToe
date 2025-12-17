using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class StartSessionRequest : IValidatableRequest
{
    public int? UserId { get; set; }
    public string GameMode { get; set; } = string.Empty;
    public string BotMode { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;
        
        bool isUserIdValid = UserId is null or > 0;
        bool isGameModeValid = !string.IsNullOrWhiteSpace(GameMode);
        bool isBotModeValid = !string.IsNullOrWhiteSpace(BotMode);

        bool isValid = isGameModeValid && isBotModeValid && isUserIdValid;

        if (!isUserIdValid) message += "User Id must be greater than 0 or null\n";
        if (!isGameModeValid) message += "Game Mode is required \n";
        if (!isBotModeValid) message += "Bot Mode is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}