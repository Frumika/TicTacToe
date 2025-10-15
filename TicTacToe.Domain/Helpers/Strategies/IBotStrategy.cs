using TicTacToe.Domain.Models.Game;

namespace TicTacToe.Domain.Helpers.Strategies;


public interface IBotStrategy
{
    (int row, int column) FindField(Board board);
}