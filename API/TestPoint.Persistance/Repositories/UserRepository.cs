using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }

    protected override IQueryable<User> GetInclusions()
    {
        return DbSet.Include(x => x.Login);
    }
}