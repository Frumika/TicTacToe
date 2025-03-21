namespace TicTacToe.API.Requests;

public class RegisterRequest
{
    public string PlayerSessionId { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}