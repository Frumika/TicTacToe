using Backend.Domain.Models.Game;

namespace Backend.Domain.Helpers.Strategies;


public interface IBotStrategy
{
    (int row, int column) FindField(Board board);
}