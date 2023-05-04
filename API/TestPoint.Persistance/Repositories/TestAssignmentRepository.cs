using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class TestAssignmentRepository : RepositoryBase<TestAssignment, Guid>, ITestAssignmentRepository
{
    public TestAssignmentRepository(DbContext context) : base(context) { }

    protected override IQueryable<TestAssignment> GetInclusions()
    {
        return DbSet.Include(x => x.TestCompletion).ThenInclude(x => x.Answers);
    }
}
