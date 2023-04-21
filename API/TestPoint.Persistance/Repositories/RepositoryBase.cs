using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity
{
    protected readonly DbSet<TEntity> DbSet;
    protected RepositoryBase(DbContext context) => DbSet = context.Set<TEntity>();

    public virtual void Add(TEntity entity) => DbSet.Add(entity);
    public virtual void Remove(TEntity entity) => DbSet.Remove(entity);
    public virtual async Task<TEntity?> GetByIdAsync(TKey id) => await GetInclusions().FirstOrDefaultAsync(x => x.Id!.Equals(id));
    public virtual async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate) => await GetInclusions().FirstOrDefaultAsync(predicate);
    public virtual async Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate) => await GetInclusions().Where(predicate).ToListAsync();
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await GetInclusions().ToListAsync();

    protected virtual IQueryable<TEntity> GetInclusions() => DbSet;
}