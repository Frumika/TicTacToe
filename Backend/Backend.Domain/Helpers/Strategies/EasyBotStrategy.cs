using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Game;
using static Backend.Domain.Enums.FieldItem;

namespace Backend.Domain.Helpers.Strategies;

public class EasyBotStrategy : BotStrategy
{
    private Random _random = new();

    public EasyBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        List<(int row, int column)> coordinates = board.GetFieldsCoordinates(Empty);

        if (coordinates.Count == 0) throw new NoEmptyFieldsException("Нет свободных полей");

        return coordinates[_random.Next(coordinates.Count)];
    }
}