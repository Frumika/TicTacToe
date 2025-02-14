namespace TicTacToe.Model.Core.GameBoard.BoardExceptions;

public class BoardExceptionNotNatural : Exception
{
    public string? Info { get; init; } = null;

    public BoardExceptionNotNatural(string? messege) : base(messege)
    {
        Info = messege;
    }
}