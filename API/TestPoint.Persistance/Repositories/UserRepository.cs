using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Application.Users;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }

    public async Task<List<UserInformationShort>> FilterUsersByFIO(string filter)
    {
        var result = await DbSet
            .AsNoTracking()
            .Where(u => EF.Functions.Like(u.LastName + " " + u.FirstName, $"%{filter}%"))
            .Select(u => new
            {
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email
            })
            .ToListAsync();

        return result
            .Select(u => new UserInformationShort(u.Id, u.FirstName, u.LastName, u.Email))
            .ToList();
    }

    protected override IQueryable<User> GetInclusions()
    {
        return DbSet.Include(x => x.Login);
    }
}