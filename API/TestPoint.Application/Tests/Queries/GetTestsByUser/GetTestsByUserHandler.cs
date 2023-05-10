using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Tests.Queries.GetTestsByUser;

public class GetTestsByUserHandler : IRequestHandler<GetTestsByUserQuery, List<TestInformation>>
{
    private readonly IUnitOfWork _uow;

    public GetTestsByUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<TestInformation>> Handle(GetTestsByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id does not exist");
        }

        Guid[] testIds;
        List<TestInformation> resultSet = null!;

        switch (request.Filter)
        {
            case UserTestsFilter.NotPassed:

                testIds = (await _uow.TestAssignmentRepository
                    .FilterByAsync(x => x.UserId == user.Id && x.TestCompletion == null))
                    .Select(x => x.TestId)
                    .ToArray();

                resultSet = await _uow.TestRepository.GetTestListById(testIds);
                break;

            case UserTestsFilter.Passed:

                testIds = (await _uow.TestAssignmentRepository
                    .FilterByAsync(x => x.UserId == user.Id && x.TestCompletion != null))
                    .Select(x => x.TestId)
                    .ToArray();

                resultSet = await _uow.TestRepository.GetTestListById(testIds);
                break;

            case UserTestsFilter.All:

                testIds = (await _uow.TestAssignmentRepository
                    .FilterByAsync(x => x.UserId == user.Id))
                    .Select(x => x.TestId)
                    .ToArray();

                resultSet = await _uow.TestRepository.GetTestListById(testIds);
                break;
        }

        return resultSet;
    }
}
