using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public enum BotMode
{
    NoMode,
    Easy,
    Medium,
    Hard
}


public class Bot
{
    private FieldItem _item;
    private IBotStrategy _strategy;


    public FieldItem Item
    {
        get => _item;
        init => _item = value;
    }


    public Bot(FieldItem item, BotMode botMode)
    {
        Item = item;

        _strategy = botMode switch
        {
            Easy => new EasyBotStrategy(Item),
            Medium => new MediumBotStrategy(Item),
            Hard => new HardBotStrategy(Item),
            _ => new MediumBotStrategy(Item)
        };
    }

    public (int row, int column) Move(Board board)
    {
        return _strategy.FindField(board);
    }
}