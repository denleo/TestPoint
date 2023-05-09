using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestResult;

public class GetTestResultHandler : IRequestHandler<GetTestResultQuery, TestCompletion>
{
    private readonly IUnitOfWork _uow;

    public GetTestResultHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<TestCompletion> Handle(GetTestResultQuery request, CancellationToken cancellationToken)
    {
        var testAssignment = await _uow.TestAssignmentRepository
            .FindOneAsync(x => x.UserId == request.UserId && x.TestId == request.TestId);

        if (testAssignment is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id is not assigned to the test with {request.TestId} id");
        }

        if (testAssignment.TestCompletion is null)
        {
            throw new EntityConflictException("The user has not passed the test yet");
        }

        return await _uow.TestCompletionRepository.GetByIdAsync(testAssignment.TestCompletion.Id);
    }
}
