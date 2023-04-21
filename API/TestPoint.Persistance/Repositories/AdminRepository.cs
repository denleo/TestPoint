using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class AdminRepository : RepositoryBase<Administrator, Guid>, IAdminRepository
{
    public AdminRepository(DbContext context) : base(context) { }

    protected override IQueryable<Administrator> GetInclusions()
    {
        return DbSet.Include(x => x.Login);
    }
}