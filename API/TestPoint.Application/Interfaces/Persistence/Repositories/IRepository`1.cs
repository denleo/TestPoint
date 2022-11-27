using System.Linq.Expressions;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAllAsync();
}