﻿namespace TicTacToe.Model.Core.Entity;


public class Bot : Entity
{
    public override string? Name { get; set; }

    public Bot(string name = "Computer") : base(name)
    {
        
    }
}