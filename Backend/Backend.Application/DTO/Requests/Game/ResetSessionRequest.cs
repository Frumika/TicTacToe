using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class ResetSessionRequest : IValidatableRequest
{
    public int? UserId { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public string GameMode { get; set; } = string.Empty;
    public string BotMode { get; set; } = string.Empty;


    public ValidationResult Validate()
    {
        string? message = null;

        bool isUserIdValid = UserId is null or > 0;
        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);
        bool isGameModeValid = !string.IsNullOrWhiteSpace(GameMode);
        bool isBotModeValid = !string.IsNullOrWhiteSpace(BotMode);

        bool isValid = isUserIdValid && isSessionIdValid && isGameModeValid && isBotModeValid;

        if (!isUserIdValid) message += "User Id must be greater than 0 or null\n";
        if (!isSessionIdValid) message += "Session Id is required \n";
        if (!isGameModeValid) message += "Game Mode is required \n";
        if (!isBotModeValid) message += "Bot Mode is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}