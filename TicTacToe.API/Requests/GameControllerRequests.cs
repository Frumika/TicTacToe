namespace TicTacToe.API.Requests;

public class MoveRequest
{
    public string? GameSessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}

public class GameInfoRequest
{
    public string? GameSessionId { get; set; }
    public string? GameMode { get; set; }
    public string? BotMode { get; set; }
}

