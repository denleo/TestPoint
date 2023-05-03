using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Cache;

public static class DependencyInjector
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration appConfig)
    {
        var cacheSettings = new RedisCacheSettings();
        appConfig.GetSection(nameof(RedisCacheSettings)).Bind(cacheSettings);
        services.AddSingleton(cacheSettings);

        if (!cacheSettings.Enabled)
        {
            return services;
        }

        services.AddStackExchangeRedisCache(x =>
        {
            x.InstanceName = "TestPointResponseCache";
            x.Configuration = cacheSettings.ConnectionString;
        });

        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        return services;
    }
}