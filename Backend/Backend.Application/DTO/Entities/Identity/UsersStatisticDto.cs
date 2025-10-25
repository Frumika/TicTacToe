namespace Backend.Application.DTO.Entities.Identity;

public class UsersStatisticDto : BaseDto
{
    public IEnumerable<UserDto>? Users { get; set; }
    public bool isLastPage { get; set; }
}