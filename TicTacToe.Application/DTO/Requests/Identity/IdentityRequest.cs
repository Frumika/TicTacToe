using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Application.DTO.Requests.Identity;

public class IdentityRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
