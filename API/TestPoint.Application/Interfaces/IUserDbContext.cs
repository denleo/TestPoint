using Microsoft.EntityFrameworkCore;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces;

public interface IUserDbContext : IDisposable
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}