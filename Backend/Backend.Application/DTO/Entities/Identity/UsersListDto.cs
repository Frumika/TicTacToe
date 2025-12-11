namespace Backend.Application.DTO.Entities.Identity;

public class UsersListDto
{
    public IEnumerable<UserDto>? Users { get; set; }
    public bool IsLastPage { get; set; }
}