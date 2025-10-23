using Backend.Domain.Enums;
using Backend.Domain.Models.Game;

namespace Backend.Domain.Helpers.Strategies;

public abstract class BotStrategy : IBotStrategy
{
    protected FieldItem Item { get; }

    protected BotStrategy(FieldItem item)
    {
        Item = item;
    }

    public abstract (int row, int column) FindField(Board board);
}