using TestPoint.Application.Tests.Queries.GetUsersOnTest;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface ITestAssignmentRepository : IRepository<TestAssignment, Guid>
{
    Task<List<UserOnTest>> GetUsersOnTest(Guid testId);
}
