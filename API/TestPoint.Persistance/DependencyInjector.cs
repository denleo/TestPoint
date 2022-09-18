using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces;
using TestPoint.DAL.Contexts;

namespace TestPoint.DAL;

public static class DependencyInjector
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddScoped<IUserDbContext, UserDbContext>();
        services.AddScoped<IAdminDbContext, AdminDbContext>();

        return services;
    }
}