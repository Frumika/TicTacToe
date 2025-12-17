using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Game;
using static Backend.Domain.Enums.FieldItem;

namespace Backend.Domain.Helpers.Strategies;

public class MediumBotStrategy : BotStrategy
{
    private Random _random = new();

    public MediumBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        int centerRow = (board.Rows - 1) / 2;
        int centerColumn = (board.Columns - 1) / 2;
        if (board.IsFieldEmpty(centerRow, centerColumn)) return (centerRow, centerColumn);

        List<(int row, int column)> emptyFieldCoordinates = board.GetFieldsCoordinates(Empty);
        if (emptyFieldCoordinates.Count == 0) throw new NoEmptyFieldsException("Нет свободных полей");

        List<(int row, int column)> botWinFields = new();
        List<(int row, int column)> playerWinFields = new();

        foreach (var field in emptyFieldCoordinates)
        {
            if (board.CanItBeWinningField(field.row, field.column, Item))
            {
                botWinFields.Add(field);
            }

            if (board.CanItBeWinningField(field.row, field.column, Item == Zero ? Cross : Zero))
            {
                playerWinFields.Add(field);
            }
        }

        if (botWinFields.Count != 0) return botWinFields[_random.Next(botWinFields.Count)];
        else if (playerWinFields.Count != 0) return playerWinFields[_random.Next(playerWinFields.Count)];

        return emptyFieldCoordinates[_random.Next(emptyFieldCoordinates.Count)];
    }
}