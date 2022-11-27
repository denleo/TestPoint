using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.DAL;

public static class DependencyInjector
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}