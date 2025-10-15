namespace TicTacToe.Domain.Models;

public class User
{
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    
    public int Matches { get; set; } 
    public int Wins { get; set; }
}