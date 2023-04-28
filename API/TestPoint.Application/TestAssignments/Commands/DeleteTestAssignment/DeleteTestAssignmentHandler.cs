using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.TestAssignments.Commands.DeleteTestAssignment;

public class DeleteTestAssignmentHandler : IRequestHandler<DeleteTestAssignmentCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteTestAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTestAssignmentCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.FindOneAsync(user => user.Id == request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id was not found");
        }

        var test = await _uow.TestRepository.FindOneAsync(test => test.Id == request.TestId);

        if (test is null)
        {
            throw new EntityNotFoundException($"Test with {request.TestId} id was not found");
        }

        var testAssignment = await _uow.TestAssignmentRepository
            .FindOneAsync(testAssignment => testAssignment.UserId == user.Id && testAssignment.TestId == test.Id);

        if (testAssignment is null)
        {
            throw new EntityNotFoundException("Test assignment was not found");
        }

        if (testAssignment.TestCompletion is not null)
        {
            throw new ActionNotAllowedException("Can't delete test assignment. User has already passed test associated with this assignment");
        }

        _uow.TestAssignmentRepository.Remove(testAssignment);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
