using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;


public class Bot  
{
    public FieldItem Item { get; init; }
    
    private IBotStrategy? _strategy = new MediumBotStrategy(Empty);

    public Bot(FieldItem item = Empty)
    {
        Item = item;
        
    }

    public (int row, int column) Move(Board board)
    {
        return _strategy.FindField(board);
    }
}