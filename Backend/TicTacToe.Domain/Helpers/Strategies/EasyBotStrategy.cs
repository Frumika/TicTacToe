using static TicTacToe.Domain.Enums.FieldItem;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Models.Game;

namespace TicTacToe.Domain.Helpers.Strategies;

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