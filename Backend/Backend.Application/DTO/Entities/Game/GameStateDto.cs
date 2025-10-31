using Backend.Domain.Enums;
using Backend.Domain.Models.Game;

namespace Backend.Application.DTO.Entities.Game;

public class GameStateDto : BaseDto
{
    public Field[][]? Board { get; set; }
    public Winner Winner { get; set; }

    
    public GameStateDto()
    {
    }
    
    public GameStateDto(Session session)
    {
        Board = session.Board;
        Winner = session.Winner;
    }
}