using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Models.Game;

namespace TicTacToe.Domain.Helpers.Strategies;

public abstract class BotStrategy : IBotStrategy
{
    protected FieldItem Item { get; }

    protected BotStrategy(FieldItem item)
    {
        Item = item;
    }

    public abstract (int row, int column) FindField(Board board);
}