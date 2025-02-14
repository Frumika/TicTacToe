using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public class Bot : Entity
{
    private IBotStrategy? _strategy = new MediumBotStrategy(Empty);

    public Bot(string name = "Computer", FieldItem item = Empty) : base(name, item)
    {
    }

    public override void Move(Board board)
    {
        _strategy.FindField(board);
    }
}