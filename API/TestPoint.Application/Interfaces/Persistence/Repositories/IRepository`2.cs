using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
    where TEntity : Entity<TKey>
{
    public Task<TEntity?> GetByIdAsync(TKey id);
}