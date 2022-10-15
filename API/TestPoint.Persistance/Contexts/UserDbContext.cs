using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.DAL.Configurations;
using TestPoint.Domain;

namespace TestPoint.DAL.Contexts;

public sealed class UserDbContext : BaseDbContext, IUserDbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(IConfiguration configuration) : base(configuration)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new LoginConfiguration())
            .ApplyConfiguration(new UserConfiguration());
    }
}