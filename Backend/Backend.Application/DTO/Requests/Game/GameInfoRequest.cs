namespace Backend.Application.DTO.Requests.Game;

public class GameInfoRequest
{
    public string GameSessionId { get; set; } = String.Empty;
    public string GameMode { get; set; } = String.Empty;
    public string BotMode { get; set; } = String.Empty;
}