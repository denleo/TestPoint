using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Tests.Commands.SubmitTestResult;

public class SubmitTestResultHandler : IRequestHandler<SubmitTestResultCommand>
{
    private readonly IUnitOfWork _uow;

    public SubmitTestResultHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(SubmitTestResultCommand request, CancellationToken cancellationToken)
    {
        var testAssignment = await _uow.TestAssignmentRepository
            .FindOneAsync(x => x.UserId == request.UserId && x.TestId == request.TestId);

        if (testAssignment is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id is not assigned to the test with {request.TestId} id");
        }

        if (testAssignment.TestCompletion is not null)
        {
            throw new EntityConflictException("The user has already passed the test");
        }

        var testCompletion = request.TestCompletion;
        testCompletion.TestAssignmentId = testAssignment.Id;
        _uow.TestCompletionRepository.Add(testCompletion);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
