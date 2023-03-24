using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class UserGroupRepository : RepositoryBase<UserGroup, Guid>, IUserGroupRepository
{
    public UserGroupRepository(DbContext context) : base(context) { }

    protected override IQueryable<UserGroup> GetInclusions()
    {
        return DbSet.Include(x => x.Users);
    }
}