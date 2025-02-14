using TicTacToe.GameModel.GameBoard;

namespace TicTacToe.GameModel.Entity;

public class Player : Entity
{
    public Player(string name, FieldItem item = Empty) : base(name, item)
    {
    }

    public override void Move(Board board)
    {
        
    }
}