using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public interface IEntity
{
    string? Name { get; init; }
    
    FieldItem? Item { get; init; }

    void Move(ref Board board);
}