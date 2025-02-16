using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public class HardBotStrategy : BotStrategy
{
    public HardBotStrategy(FieldItem item) : base(item)
    {
    }

    public override (int row, int column) FindField(Board board)
    {
        return (1, 1);
    }
}