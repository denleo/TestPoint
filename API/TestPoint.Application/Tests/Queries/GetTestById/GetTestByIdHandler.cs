using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestById;

public class GetTestByIdHandler : IRequestHandler<GetTestByIdQuery, Test>
{
    private readonly IUnitOfWork _uow;

    public GetTestByIdHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Test> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
    {
        var testData = await _uow.TestRepository.GetByIdAsync(request.TestId);

        if (testData is null)
        {
            throw new EntityNotFoundException($"Test with {request.TestId} id does not exist");
        }

        if (request.Role == LoginType.User)
        {
            var assignment = await _uow.TestAssignmentRepository
                .FindOneAsync(x => x.UserId == request.LoginId && x.TestId == request.TestId);

            if (assignment is null)
            {
                throw new ActionNotAllowedException($"User is not assigned to the test");
            }

            if (assignment.TestCompletion is null)
            {
                testData.Questions
                    .SelectMany(x => x.Answers)
                    .ToList()
                    .ForEach(x => x.IsCorrect = null);
            }
        }

        return testData;
    }
}
