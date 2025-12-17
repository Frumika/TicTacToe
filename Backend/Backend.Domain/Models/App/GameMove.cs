using Backend.Domain.Enums;

namespace Backend.Domain.Models.App;

public class GameMove
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string GameSessionId { get; set; } = string.Empty;
    public int ResetCount { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public FieldItem PlayerItem { get; set; } = FieldItem.Empty;
    public DateTime CreatedAt { get; set; }
}