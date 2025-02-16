using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public interface IBotStrategy
{
    (int row, int column) FindField(Board board);
}