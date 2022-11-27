using TestPoint.Application.Interfaces.Persistence.Repositories;

namespace TestPoint.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    public IAdminRepository AdminRepository { get; }
    public IUserRepository UserRepository { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}