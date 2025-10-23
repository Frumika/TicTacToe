using TicTacToe.Application.DTO.Entities;
using TicTacToe.Application.Enums;

namespace TicTacToe.Application.DTO.Responses.Identity;

public class UsersStatisticsResponse : BaseResponse<IdentityStatusCode>
{
    public List<UserDto>? Users { get; set; }
    public bool? IsLastPage { get; set; }

    public static UsersStatisticsResponse Success(IEnumerable<UserDto> users, bool isLastPage, string? message = null)
    {
        return new UsersStatisticsResponse
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatusCode.Success,
            Users = users.ToList(),
            IsLastPage = isLastPage
        };
    }

    public static UsersStatisticsResponse Fail(IdentityStatusCode code, string? message = null)
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