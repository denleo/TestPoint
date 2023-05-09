using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Tests.Queries.GetUsersOnTest;

public class GetUsersOnTestHandler : IRequestHandler<GetUsersOnTestQuery, List<UserOnTest>>
{
    private readonly IUnitOfWork _uow;

    public GetUsersOnTestHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<UserOnTest>> Handle(GetUsersOnTestQuery request, CancellationToken cancellationToken)
    {
        var test = await _uow.TestRepository.GetByIdAsync(request.TestId);

        if (test is null)
        {
            throw new EntityNotFoundException($"Test with {request.TestId} id does not exist");
        }

        return await _uow.TestAssignmentRepository.GetUsersOnTest(test.Id);
    }
}
