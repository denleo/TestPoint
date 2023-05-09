using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class TestCompletionRepository : RepositoryBase<TestCompletion, Guid>, ITestCompletionRepository
{
    public TestCompletionRepository(DbContext context) : base(context) { }

    protected override IQueryable<TestCompletion> GetInclusions()
    {
        return DbSet.Include(x => x.Answers);
    }
}
