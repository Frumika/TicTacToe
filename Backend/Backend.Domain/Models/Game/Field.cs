using Backend.Domain.Enums;

namespace Backend.Domain.Models.Game;


public class Field
{
    private int? _row = null;
    private int? _column = null;

    
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
    
    public FieldItem Item { get; set; } = FieldItem.Empty;

    
    public Field()
    {
    }

    public Field(Field other)
    {
        Row = other.Row;
        Column = other.Column;
        Item = other.Item;
    }

    
    public bool IsEmpty() => Item == FieldItem.Empty;
    public bool IsCross() => Item == FieldItem.Cross;
    public bool IsZero() => Item == FieldItem.Zero;
}