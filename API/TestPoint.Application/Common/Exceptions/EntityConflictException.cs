namespace TestPoint.Application.Common.Exceptions;

public class EntityConflictException : Exception
{
    public EntityConflictException(string message) : base(message)
    {
    }
}