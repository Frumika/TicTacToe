using Backend.Domain.Enums;

namespace Backend.Domain.Models.Game;

public class Field
{
    public int Row { get; set; }
    public int Column { get; set; }
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