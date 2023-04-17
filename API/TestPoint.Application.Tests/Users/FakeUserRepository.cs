using System.Linq.Expressions;
using TestPoint.Application.Interfaces.Persistence.Repositories;

namespace TestPoint.Application.Tests.Users;

public class FakeUserRepository : IUserRepository
{
    public List<User> Users { get; set; }

    public void Add(User entity) { }
    public void Remove(User entity) { }

    public Task<IEnumerable<User>> FilterByAsync(Expression<Func<User, bool>> predicate)
    {
        var users = Users.Where(predicate.Compile());
        return Task.FromResult(users);
    }

    public Task<User?> FindOneAsync(Expression<Func<User, bool>> predicate)
    {
        var user = Users.Where(predicate.Compile()).FirstOrDefault();
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(Users.AsEnumerable());
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        var user = Users.Where(x => x.Id == id).FirstOrDefault();
        return Task.FromResult(user);
    }
}
