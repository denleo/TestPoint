using Microsoft.Extensions.Logging;

namespace TestPoint.Application.Interfaces.Services;

public interface ILogService
{
    void Log<TClass>(LogLevel logLevel, string logMessage)
        where TClass : class;

    void Log<TClass>(LogLevel logLevel, string logMessage, Exception exception)
        where TClass : class;
}
