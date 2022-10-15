using log4net;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Log4NetLoggly;

public static class DependencyInjector
{
    public static IServiceCollection AddLoggly(this IServiceCollection services)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("App.config"));

        services.AddSingleton<ILogService, LoggerAdapter>();
        return services;
    }
}