using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Identity;

public class ChangeUserDataRequest : IValidatableRequest
{
    public string OldLogin { get; set; } = string.Empty;
    public string NewLogin { get; set; } = string.Empty;

    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }

    public ValidationResult Validate()
    {
        string? message = null;

        bool isOldLoginValid = !string.IsNullOrWhiteSpace(OldLogin);
        bool isNewLoginValid = !string.IsNullOrWhiteSpace(NewLogin);
        bool isWinsValid = Wins >= 0;
        bool isLossesValid = Losses >= 0;
        bool isDrawsValid = Draws >= 0;
        bool isValid = isOldLoginValid && isNewLoginValid && isWinsValid && isLossesValid && isDrawsValid;

        if (!isOldLoginValid) message += "Old login is required\n";
        if (!isNewLoginValid) message += "New login is required\n";
        if (!isWinsValid) message += "Wins must be in the range from 0 to 2_147_483_647\n";
        if (!isLossesValid) message += "Losses must be in the range from 0 to 2_147_483_647\n";
        if (!isDrawsValid) message += "Draws must be in the range from 0 to 2_147_483_647";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}