﻿namespace TicTacToe.GameModel.GameBoard;


public class Field
{
    /*------------| < Private Fields > |------------*/
    private int? _row = null;
    private int? _column = null;
    /*----------------------------------------------*/
    
    
    /*--------------| < Properties > |--------------*/
    public int Row
    {
        get => _row ?? -1;
        set
        {
            if (_row is null) _row = value;
        }
    }

    public int Column
    {
        get => _column ?? -1;
        set
        {
            if (_column is null) _column = value;
        }
    }
    
    public FieldItem Item { get; set; } = Empty;
    /*----------------------------------------------*/
    
    
    /*--------------| < Constructors > |------------*/
    public Field()
    {
    }

    public Field(Field other)
    {
        Row = other.Row;
        Column = other.Column;
        Item = other.Item;
    }
    /*----------------------------------------------*/

    
    /*------------| < Public Methods > |------------*/
    public bool IsEmpty() => Item == Empty;
    public bool IsCross() => Item == Cross;
    public bool IsZero() => Item == Zero;
    /*----------------------------------------------*/

}