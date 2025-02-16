using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class NoEmptyFields : Exception
{
    public NoEmptyFields(string messege) : base(messege)
    {
    }
}

public abstract class BotStrategy : IBotStrategy
{
    protected FieldItem Item;

    public BotStrategy(FieldItem item)
    {
        Item = item;
    }

    public abstract (int row, int column) FindField(Board board);
}

