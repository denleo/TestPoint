namespace TestPoint.Application.Common.Exceptions;

public class EmailConfirmationException : Exception
{
    public EmailConfirmationException(string message) : base(message)
    {
    }
}