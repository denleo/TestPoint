using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.DAL.Contexts;

namespace TestPoint.DAL;

public static class DependencyInjector
{
    public static IServiceCollection AddDal(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}