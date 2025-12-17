using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Admin;

public class UpdateUserDataRequest : IValidatableRequest
{
    public int UserId { get; set; }
    public string UserLogin { get; set; } = string.Empty;

    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }

    public ValidationResult Validate()
    {
        string? message = null;

        bool isUserIdValid = UserId > 0;
        bool isUserLoginValid = !string.IsNullOrWhiteSpace(UserLogin);
        bool isWinsValid = Wins >= 0;
        bool isLossesValid = Losses >= 0;
        bool isDrawsValid = Draws >= 0;

        bool isValid = isUserIdValid &&
                       isUserLoginValid &&
                       isWinsValid &&
                       isLossesValid &&
                       isDrawsValid;

        if (!isUserIdValid) message += "User Id must be greater than 0\n";
        if (!isUserLoginValid) message += "User Login is required\n";
        if (!isWinsValid) message += "Wins must be in the range from 0 to 2_147_483_647\n";
        if (!isLossesValid) message += "Losses must be in the range from 0 to 2_147_483_647\n";
        if (!isDrawsValid) message += "Draws must be in the range from 0 to 2_147_483_647";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}