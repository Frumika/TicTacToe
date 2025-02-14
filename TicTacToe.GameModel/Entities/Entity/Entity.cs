using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public abstract class Entity : IEntity
{
    public FieldItem Item { get; init; }

    public string? Name { get; init; }


    public Entity(string name, FieldItem item = Empty)
    {
        Name = name;
        Item = item;
    }

    public abstract void Move(Board board);

}