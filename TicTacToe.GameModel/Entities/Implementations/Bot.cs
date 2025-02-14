namespace TicTacToe.GameModel.Entity;

public class Bot : Entity
{
    public override string? Name { get; set; }

    public Bot(string name = "Computer") : base(name)
    {
        
    }
}