using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public class EasyBotStrategy : BotStrategy
{
    private Random _random = new();

    public EasyBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        List<(int row, int column)> coordinates = board.GetFieldsCoordinates(Empty);

        if (coordinates.Count == 0) throw new NoEmptyFields("Нет свободных полей");

        return coordinates[_random.Next(coordinates.Count)];
    }
}
