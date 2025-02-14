using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public class MediumBotStrategy : BotStrategy
{
    private Random _random = new();

    public MediumBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        List<(int row, int column)> coordinates = board.GetFieldsCoordinates(Empty);

        if (coordinates.Count == 0) throw new NoEmptyFields("Нет свободных полей");

        foreach (var field in coordinates)
        {
            if (board.CheckFieldForWin(field.row, field.column, Item)) return field;
        }

        return coordinates[_random.Next(coordinates.Count)];
    }
}


