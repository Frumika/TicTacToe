using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}