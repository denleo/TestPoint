namespace TestPoint.Application.Common.Exceptions;

public class BadEntityException : Exception
{
    public BadEntityException(string message) : base(message)
    {
    }
}
