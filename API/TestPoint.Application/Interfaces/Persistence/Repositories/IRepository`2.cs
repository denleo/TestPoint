using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
    where TEntity : Entity
{
    public Task<TEntity?> GetByIdAsync(TKey id);
}