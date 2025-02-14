namespace TicTacToe.GameModel.GameBoard.BoardException;

public class BoardExceptionNotNatural : Exception
{
    public string? Info { get; init; } = null;

    public BoardExceptionNotNatural(string? messege) : base(messege)
    {
        Info = messege;
    }
}