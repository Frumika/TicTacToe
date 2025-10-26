using Backend.Application.DTO.Requests.Base;
using Backend.Application.Enums;
using static Backend.Application.Enums.StatisticType;


namespace Backend.Application.DTO.Requests.Identity;

public class GetUsersStatisticsRequest : IValidatableRequest
{
    public int SkipModifier { get; set; }
    public int UsersCount { get; set; }
    public string StringStatisticType { get; set; } = string.Empty;

    public StatisticType Type
    {
        get
        {
            StatisticType type = StringStatisticType switch
            {
                "ByMatches" => ByMatches,
                "ByWins" => ByWins,
                "ByLosses" => ByLosses,
                _ => ByMatches
            };

            return type;
        }
    }

    public ValidationResult Validate()
    {
        string? message = null;

        bool isSkipModifierValid = SkipModifier >= 0;
        bool isUserCountValid = UsersCount >= 1;
        bool isStatisticTypeValid = !string.IsNullOrWhiteSpace(StringStatisticType);
        bool isValid = isSkipModifierValid && isUserCountValid && isStatisticTypeValid;

        if (!isSkipModifierValid) message += "SkipModifier must be in the range from 0 to 2_147_483_647 \n";
        if (!isUserCountValid) message += "UserCount must be in the range from 1 to 2_147_483_647 \n";
        if (!isStatisticTypeValid) message += "StringStatisticType is required";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}