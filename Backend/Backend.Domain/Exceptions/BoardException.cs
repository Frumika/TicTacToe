namespace Backend.Domain.Exceptions;

public class BoardExceptionNotNatural : Exception
{
    public string? Info { get; init; } = null;

    public BoardExceptionNotNatural(string? message) : base(message)
    {
        Info = message;
    }
}