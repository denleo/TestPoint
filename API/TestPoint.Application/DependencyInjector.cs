using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestPoint.Application.Common.Authentication;
using TestPoint.Application.Interfaces;

namespace TestPoint.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IJwtService, JwtService>();
        return services;
    }
}