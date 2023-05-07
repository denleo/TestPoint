using TestPoint.Application.Tests;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface ITestAssignmentRepository : IRepository<TestAssignment, Guid>
{
    Task<List<UserOnTest>> GetUsersOnTest(Guid testId);
}
