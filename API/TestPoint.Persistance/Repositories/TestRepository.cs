using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class TestRepository : RepositoryBase<Test, Guid>, ITestRepository
{
    public TestRepository(DbContext context) : base(context) { }

    protected override IQueryable<Test> GetInclusions()
    {
        return DbSet.Include(x => x.Questions).ThenInclude(x => x.Answers);
    }
}
