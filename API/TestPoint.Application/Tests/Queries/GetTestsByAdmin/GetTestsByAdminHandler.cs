using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Tests.Queries.GetTestsByAdmin;

public class GetTestsByAdminHandler : IRequestHandler<GetTestsByAdminQuery, List<TestInformation>>
{
    private readonly IUnitOfWork _uow;

    public GetTestsByAdminHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<TestInformation>> Handle(GetTestsByAdminQuery request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository.GetByIdAsync(request.AdminId);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Administrator with {request.AdminId} id does not exist");
        }

        var tests = await _uow.TestRepository.FilterByAsync(x => x.AuthorId == admin.Id);

        return tests
            .Select(t => new TestInformation(t.Id, admin.Login.Username, t.Name, t.Difficulty, t.Questions.Count(), t.EstimatedTime))
            .ToList();
    }
}
