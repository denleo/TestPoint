using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestPoint.Application.Interfaces;
using TestPoint.DAL.Configurations;
using TestPoint.Domain;

namespace TestPoint.DAL.Contexts;

public sealed class AdminDbContext : BaseDbContext, IAdminDbContext
{
    public DbSet<Administrator> Administrators { get; set; }

    public AdminDbContext(IConfiguration configuration) : base(configuration)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new LoginConfiguration())
            .ApplyConfiguration(new AdminConfiguration());
    }
}