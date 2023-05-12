using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Application.Users;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }

    public async Task<List<UserInformation>> FilterUsersByFIO(string filter)
    {
        return await DbSet
            .AsNoTracking()
            .Where(u => EF.Functions.Like(u.LastName + " " + u.FirstName, $"%{filter}%"))
            .Select(u => new UserInformation(u.Id, u.FirstName, u.LastName, u.Email, u.Avatar != null ? Convert.ToBase64String(u.Avatar) : null))
            .ToListAsync();
    }

    protected override IQueryable<User> GetInclusions()
    {
        return DbSet.Include(x => x.Login);
    }
}