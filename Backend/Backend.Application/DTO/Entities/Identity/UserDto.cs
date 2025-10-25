using Backend.Domain.Models.App;

namespace Backend.Application.DTO.Entities.Identity;

public class UserDto : BaseDto
{
    public string Login { get; set; } = string.Empty;
    public int Matches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }

    public UserDto()
    {
    }

    public UserDto(User user)
    {
        Login = user.Login;
        Matches = user.Matches;
        Wins = user.Wins;
        Losses = user.Matches - user.Wins;
    }
}