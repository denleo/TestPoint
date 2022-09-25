using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestPoint.DAL.Contexts;

public abstract class BaseDbContext : DbContext
{
    protected readonly IConfiguration AppConfiguration;

    protected BaseDbContext(IConfiguration configuration) => AppConfiguration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.GetConnectionString("LocalConnection"));
        }
    }
}