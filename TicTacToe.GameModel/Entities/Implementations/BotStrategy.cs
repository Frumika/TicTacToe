using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class NoEmptyFields : Exception
{
    public NoEmptyFields(string messege) : base(messege)
    {
    }
}

public class EasyBotStrategy : IBotStrategy
{
    private Random _random = new();

    public (int row, int column) FindField(Board board)
    {
        List<(int row, int column)> coordinates = board.GetFieldsCoordinates(Empty);

        if (coordinates.Count == 0) throw new NoEmptyFields("Нет свободных полей");

        return coordinates[_random.Next(coordinates.Count)];
    }
}

public class MediumBotStrategy : IBotStrategy
{
    private Random _random = new();

    public (int row, int column) FindField(Board board)
    {
        return (1, 1);
    }
}

public class HardBotStrategy : IBotStrategy
{
    public (int row, int column) FindField(Board board)
    {
        return (1, 1);
    }
}