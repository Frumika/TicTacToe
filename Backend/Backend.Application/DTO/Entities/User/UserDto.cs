namespace Backend.Application.DTO.Entities.User;

public class UserDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public int Matches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
    public bool IsAdmin { get; set; }

    public UserDto()
    {
    }

    public UserDto(Domain.Models.App.User user)
    {
        Id = user.Id;
        Login = user.Login;
        Matches = user.Matches;
        Wins = user.Wins;
        Losses = user.Losses;
        Draws = user.Draws;
        IsAdmin = user.IsAdmin;
    }
}