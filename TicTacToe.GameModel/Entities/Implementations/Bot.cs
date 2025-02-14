using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class Bot : Entity
{
    private IBotStrategy? _strategy = null;

    public Bot(string name = "Computer", FieldItem item = Empty) : base(name, item)
    {
    }

    public override void Move(ref Board board)
    {
    }
}