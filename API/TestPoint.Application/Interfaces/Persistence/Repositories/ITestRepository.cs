using TestPoint.Application.Tests;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface ITestRepository : IRepository<Test, Guid>
{
    Task<List<TestInformation>> GetTestListByAuthor(Guid authorId);
    Task<List<TestInformation>> GetTestListById(params Guid[] ids);
}
