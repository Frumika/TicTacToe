namespace Backend.Domain.Exceptions;

public class NoEmptyFieldsException : Exception
{
    public NoEmptyFieldsException(string message) : base(message)
    {
    }
}
