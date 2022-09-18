namespace TestPoint.Application.Common.Exceptions;

public class EntityExistsException : Exception
{
    public EntityExistsException(string message) : base(message)
    {
    }
}