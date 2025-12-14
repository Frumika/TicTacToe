namespace Backend.Application.DTO.Entities.User;

public class UserSessionStateDto
{
    public int UserId { get; init; }
    public string Login { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}