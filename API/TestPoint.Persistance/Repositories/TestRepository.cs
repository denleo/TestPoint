using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Application.Tests;
using TestPoint.Domain;

namespace TestPoint.DAL.Repositories;

public class TestRepository : RepositoryBase<Test, Guid>, ITestRepository
{
    public async Task<List<TestInformation>> GetTestListByAuthor(Guid authorId)
    {
        return await DbSet
            .Include(x => x.Questions)
            .Where(x => x.AuthorId == authorId)
            .Select(t => new TestInformation(t.Id, t.Author, t.Name, t.Difficulty, t.Questions.Count(), t.EstimatedTime))
            .ToListAsync();
    }

    public async Task<List<TestInformation>> GetTestListById(params Guid[] ids)
    {
        return await DbSet
            .Include(x => x.Questions)
            .Where(x => ids.Contains(x.Id))
            .Select(t => new TestInformation(t.Id, t.Author, t.Name, t.Difficulty, t.Questions.Count(), t.EstimatedTime))
            .ToListAsync();
    }

    public TestRepository(DbContext context) : base(context) { }

    protected override IQueryable<Test> GetInclusions()
    {
        return DbSet.Include(x => x.Questions).ThenInclude(x => x.Answers);
    }
}
