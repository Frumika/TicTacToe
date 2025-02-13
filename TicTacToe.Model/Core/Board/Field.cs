namespace TicTacToe.Model.Core.Board;

public class Field
{
    public FieldStatus Status { get; set; } = FieldStatus.Empty;

    public bool IsEmpty() => Status == FieldStatus.Empty;
    public bool IsCross() => Status == FieldStatus.Cross;
    public bool IsZero() => Status == FieldStatus.Zero;
}