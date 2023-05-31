using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.DAL.Contexts;

namespace TestPoint.DAL;

public static class DependencyInjector
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration appConfig)
    {
        var connectionString = appConfig.GetSection("DatabaseSettings:ConnectionString").Value;

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString), "Database connection string cannot be null");
        }

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}