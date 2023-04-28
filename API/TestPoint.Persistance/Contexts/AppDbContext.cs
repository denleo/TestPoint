using Microsoft.EntityFrameworkCore;
using TestPoint.DAL.Configurations;
using TestPoint.DAL.Interceptors;

namespace TestPoint.DAL.Contexts;

public sealed class AppDbContext : DbContext
{
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
            .ApplyConfiguration(new AnswerConfiguration())
            .ApplyConfiguration(new TestAssignmentConfiguration())
            .ApplyConfiguration(new TestCompletionConfiguration())
            .ApplyConfiguration(new AnswerHistoryConfiguration());
    }
}