using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Cache;

public static class DependencyInjector
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration appConfig)
    {
        var cacheSettings = appConfig.GetSection(nameof(RedisCacheSettings)).Get<RedisCacheSettings>();

        if (cacheSettings is null)
        {
            return services;
        }

        services.AddStackExchangeRedisCache(x =>
        {
            x.InstanceName = "TestPointResponseCache";
            x.Configuration = cacheSettings.ConnectionString;
        });

        services.Configure<RedisCacheSettings>(appConfig.GetSection(nameof(RedisCacheSettings)));
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        return services;
    }
}