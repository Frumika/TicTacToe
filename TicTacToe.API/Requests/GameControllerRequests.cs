namespace TicTacToe.API.Requests;

public class MoveRequest
{
    public string? GameSessionId { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }
}

public class GameInfoRequest
{
    public string GameSessionId { get; set; } = String.Empty;
    public string GameMode { get; set; } = String.Empty;
    public string BotMode { get; set; } = String.Empty;
}