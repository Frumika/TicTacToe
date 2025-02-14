namespace TicTacToe.GameModel.Entity;


public class Guest : Entity
{
    public override string? Name { get; set; }

    public Guest(string name) : base(name)
    {
    }
}