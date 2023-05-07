using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Application.Tests;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class TestAssignmentRepository : RepositoryBase<TestAssignment, Guid>, ITestAssignmentRepository
{
    public TestAssignmentRepository(DbContext context) : base(context) { }

    public async Task<List<UserOnTest>> GetUsersOnTest(Guid testId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.TestCompletion)
            .Where(x => x.TestId == testId)
            .Select(x => new UserOnTest(
                    x.UserId,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.Email,
                    x.TestCompletion != null)
            )
            .ToListAsync();
    }

    protected override IQueryable<TestAssignment> GetInclusions()
    {
        return DbSet.Include(x => x.User).Include(x => x.Test).Include(x => x.TestCompletion);
    }
}
