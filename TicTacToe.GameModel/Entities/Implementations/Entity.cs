namespace TicTacToe.GameModel.Entity;


public class Entity : IEntity
{
    public virtual string? Name { get; set; }

    
    public Entity(string name)
    {
        Name = name;
    }
}