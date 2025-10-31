using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class ResetSessionRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;
    public string GameMode { get; set; } = string.Empty;
    public string BotMode { get; set; } = string.Empty;


    public ValidationResult Validate()
    {
        string? message = null;

        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);
        bool isGameModeValid = !string.IsNullOrWhiteSpace(GameMode);
        bool isBotModeValid = !string.IsNullOrWhiteSpace(BotMode);

        bool isValid = isSessionIdValid && isGameModeValid && isBotModeValid;

        if (!isSessionIdValid) message += "Session Id is required \n";
        if (!isGameModeValid) message += "Game Mode is required \n";
        if (!isBotModeValid) message += "Bot Mode is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}