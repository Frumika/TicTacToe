using TicTacToe.Application.DTO.Entities;
using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class UsersStatisticsResponse : BaseResponse<IdentityStatus>
{
    List<UserDto>? Users { get; set; }
    bool? IsLastPage { get; set; }

    public static UsersStatisticsResponse Success(IEnumerable<UserDto> users, bool isLastPage, string? message = null)
    {
        return new UsersStatisticsResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success,
            Users = users.ToList(),
            IsLastPage = isLastPage
        };
    }

    public static UsersStatisticsResponse Fail(IdentityStatus code, string? message = null)
    {
        return new UsersStatisticsResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Users = null,
            IsLastPage = null
        };
    }
}