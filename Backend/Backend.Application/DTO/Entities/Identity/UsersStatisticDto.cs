namespace Backend.Application.DTO.Entities.Identity;

public class UsersStatisticDto
{
    public IEnumerable<UserDto>? Users { get; set; }
    public bool IsLastPage { get; set; }
}