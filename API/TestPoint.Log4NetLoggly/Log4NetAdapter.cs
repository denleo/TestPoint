using log4net;
using Microsoft.Extensions.Logging;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Log4NetLoggly;

public class Log4NetAdapter : ILogService
{
    public void Log<T>(LogLevel logLevel, string logMessage, Exception? exception = null)
        where T : class
    {
        var ex = exception ?? new Exception("Unknown");

        var log = LogManager.GetLogger(typeof(T));
        switch (logLevel)
        {
            case LogLevel.Debug:
                log.Debug(logMessage, ex);
                break;
            case LogLevel.Trace:
            case LogLevel.Information:
                log.Info(logMessage, ex);
                break;
            case LogLevel.Warning:
                log.Warn(logMessage, ex);
                break;
            case LogLevel.Error:
                log.Error(logMessage, ex);
                break;
            case LogLevel.Critical:
                log.Fatal(logMessage, ex);
                break;
        }
    }
}
