namespace TicTacToe.Model.Core.Board;

public class Field
{
    /*  Private Fields  */
    private int? _row = null;
    private int? _column = null;

    public Field()
    {
    }

    public Field(Field other)
    {
        Row = other.Row;
        Column = other.Column;
        Item = other.Item;
    }

    /*  Properties  */
    public int Row
    {
        get => _row ?? 0;
        set => _row = value;
    }

    public int Column
    {
        get => _column ?? 0;
        set => _column = value;
    }

    public FieldItem Item { get; set; } = Empty;

    public bool IsEmpty() => Item == Empty;
    public bool IsCross() => Item == Cross;
    public bool IsZero() => Item == Zero;
}