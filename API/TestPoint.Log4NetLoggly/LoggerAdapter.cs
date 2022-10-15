using log4net;
using Microsoft.Extensions.Logging;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Log4NetLoggly;

public class LoggerAdapter : ILogService
{
    public void Log<T>(LogLevel logLevel, string logMessage)
        where T : class
    {
        var log = LogManager.GetLogger(typeof(T));

        switch (logLevel)
        {
            case LogLevel.Debug:
                log.Debug(logMessage);
                break;
            case LogLevel.Trace:
            case LogLevel.Information:
                log.Info(logMessage);
                break;
            case LogLevel.Warning:
                log.Warn(logMessage);
                break;
            case LogLevel.Error:
                log.Error(logMessage);
                break;
            case LogLevel.Critical:
                log.Fatal(logMessage);
                break;
        }
    }

    public void Log<T>(LogLevel logLevel, string logMessage, Exception exception)
        where T : class
    {
        var log = LogManager.GetLogger(typeof(T));

        switch (logLevel)
        {
            case LogLevel.Debug:
                log.Debug(logMessage, exception);
                break;
            case LogLevel.Trace:
            case LogLevel.Information:
                log.Info(logMessage, exception);
                break;
            case LogLevel.Warning:
                log.Warn(logMessage, exception);
                break;
            case LogLevel.Error:
                log.Error(logMessage, exception);
                break;
            case LogLevel.Critical:
                log.Fatal(logMessage, exception);
                break;
        }
    }
}
