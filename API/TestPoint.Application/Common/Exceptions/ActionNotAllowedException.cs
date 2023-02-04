namespace TestPoint.Application.Common.Exceptions;

public class ActionNotAllowedException : Exception
{
    public ActionNotAllowedException(string message) : base(message)
    {
    }
}