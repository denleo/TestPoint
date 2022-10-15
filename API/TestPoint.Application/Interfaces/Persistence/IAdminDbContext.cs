using Microsoft.EntityFrameworkCore;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence;

public interface IAdminDbContext : IDisposable
{
    DbSet<Administrator> Administrators { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}