using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Identity;

public class GetUsersListRequest : IValidatableRequest
{
    public int SkipModifier { get; set; }
    public int UsersCount { get; set; }


    public ValidationResult Validate()
    {
        string? message = null;

        bool isSkipModifierValid = SkipModifier >= 0;
        bool isUserCountValid = UsersCount >= 1;
        bool isValid = isSkipModifierValid && isUserCountValid;

        if (!isSkipModifierValid) message += "SkipModifier must be in the range from 0 to 2_147_483_647 \n";
        if (!isUserCountValid) message += "UserCount must be in the range from 1 to 2_147_483_647 \n";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}