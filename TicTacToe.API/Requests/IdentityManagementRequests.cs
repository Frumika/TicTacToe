using System.ComponentModel.DataAnnotations;
using static TicTacToe.API.Requests.StatisticType;

namespace TicTacToe.API.Requests;

public class IdentityRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}


public enum StatisticType
{
    ByMatches,
    ByWins,
    ByLosses
}

public class UsersStatisticsRequest
{
    [Required(ErrorMessage = "SkipModifier is required")]
    [Range(0, int.MaxValue, ErrorMessage = "SkipModifier must be in the range from 0 to 2_147_483_647")]
    public int SkipModifier { get; set; }

    [Required(ErrorMessage = "UsersCount is required")]
    [Range(1, int.MaxValue, ErrorMessage = "UserCount must be in the range from 1 to 2_147_483_647")]
    public int UsersCount { get; set; }

    [Required(ErrorMessage = "StringStatisticType is required")]
    public string StringStatisticType { get; set; }

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
}


public class UpdateDataRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = String.Empty;

    [Required(ErrorMessage = "IsWin is required")]
    public bool IsWin { get; set; } = default;
}