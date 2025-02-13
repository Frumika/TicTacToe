namespace TicTacToe.Model.Core.Entity;

public class Player : Entity
{
    public override string? Name { get; set; }

    public Player(string name) : base(name)
    {
    }
}