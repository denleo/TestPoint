using log4net;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Log4NetLoggly;

public static class DependencyInjector
{
    public static IServiceCollection AddLogService(this IServiceCollection services)
    {
        ConfigureLog4Net();

        services.AddSingleton<ILogService, Log4NetAdapter>();
        return services;
    }

    private static void ConfigureLog4Net()
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }
}