using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.JwtService;

public static class DependencyInjector
{
    public static IServiceCollection AddJwtService(this IServiceCollection services)
    {
        services.AddSingleton<IJwtService, JwtService>();

        return services;
    }
}