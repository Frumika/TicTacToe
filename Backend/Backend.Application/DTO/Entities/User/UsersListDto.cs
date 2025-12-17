namespace Backend.Application.DTO.Entities.User;

public class UsersListDto
{
    public IEnumerable<UserDto>? Users { get; set; }
    public bool IsLastPage { get; set; }
}