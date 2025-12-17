using Backend.Domain.Enums;
using Backend.Domain.Helpers.Strategies;

namespace Backend.Domain.Models.Game;

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
            BotMode.Easy => new EasyBotStrategy(Item),
            BotMode.Medium => new MediumBotStrategy(Item),
            BotMode.Hard => new HardBotStrategy(Item),
            _ => new MediumBotStrategy(Item)
        };
    }

    
    public (int row, int column) Move(Board board)
    {
        return _strategy.FindField(board);
    }
}