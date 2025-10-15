namespace TicTacToe.Application.DTO.Requests.Game;

public class MoveRequest
{
    public string? GameSessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}