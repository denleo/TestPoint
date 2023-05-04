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
        var databaseSettings = new DatabaseSettings();
        appConfig.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);
        services.AddSingleton(databaseSettings);

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(databaseSettings.ConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}