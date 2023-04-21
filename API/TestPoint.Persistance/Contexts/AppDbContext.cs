using Microsoft.EntityFrameworkCore;
using TestPoint.DAL.Configurations;
using TestPoint.DAL.Interceptors;
using TestPoint.Domain;

namespace TestPoint.DAL.Contexts;

public sealed class AppDbContext : DbContext
{
    public DbSet<SystemLogin> Logins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Admins { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Test> Tests { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new OnSaveInterceptor());

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new LoginConfiguration())
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new AdminConfiguration())
            .ApplyConfiguration(new UserGroupConfiguration())
            .ApplyConfiguration(new TestConfiguration())
            .ApplyConfiguration(new QuestionConfiguration())
            .ApplyConfiguration(new AnswerConfiguration());
    }
}