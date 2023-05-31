using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.JwtService;

public static class DependencyInjector
{
    public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration appConfig)
    {
        services.Configure<JwtAuthSettings>(appConfig.GetSection(nameof(JwtAuthSettings)));
        services.AddSingleton<IJwtService, JwtService>();

        return services;
    }
}