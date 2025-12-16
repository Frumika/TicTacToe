using Backend.Domain.Enums;
using Backend.Domain.Models.Game;


namespace Backend.Domain.DTO;

public class SessionState
{
    public int? UserId { get; set; }
    
    public Field[][]? Board { get; set; }
    public FieldItem CurrentItem { get; set; }
    public Winner Winner { get; set; }

    public GameMode GameMode { get; set; }
    public BotMode BotMode { get; set; }
}