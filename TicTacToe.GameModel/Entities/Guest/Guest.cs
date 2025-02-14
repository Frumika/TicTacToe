using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class Guest : Entity
{
    public Guest(string name, FieldItem item = Empty) : base(name, item)
    {
    }

    
    public override void Move(Board board)
    {
        
    }
}