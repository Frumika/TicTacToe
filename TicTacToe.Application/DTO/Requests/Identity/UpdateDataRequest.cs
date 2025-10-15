using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Application.DTO.Requests.Identity;

public class UpdateDataRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = String.Empty;

    [Required(ErrorMessage = "IsWin is required")]
    public bool IsWin { get; set; } = default;
}