using System.ComponentModel.DataAnnotations;
using TicTacToe.Application.Enums;
using static TicTacToe.Application.Enums.StatisticType;

namespace TicTacToe.Application.DTO.Requests.Identity;

public class UsersStatisticsRequest
{
    [Required(ErrorMessage = "SkipModifier is required")]
    [Range(0, int.MaxValue, ErrorMessage = "SkipModifier must be in the range from 0 to 2_147_483_647")]
    public int SkipModifier { get; set; }

    [Required(ErrorMessage = "UsersCount is required")]
    [Range(1, int.MaxValue, ErrorMessage = "UserCount must be in the range from 1 to 2_147_483_647")]
    public int UsersCount { get; set; }

    [Required(ErrorMessage = "StringStatisticType is required")]
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
}