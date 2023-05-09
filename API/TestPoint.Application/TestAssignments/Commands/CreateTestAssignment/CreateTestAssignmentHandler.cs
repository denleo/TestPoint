using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.TestAssignments.Commands.CreateTestAssignment;

internal class CreateTestAssignmentHandler : IRequestHandler<CreateTestAssignmentCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateTestAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(CreateTestAssignmentCommand request, CancellationToken cancellationToken)
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

        var existingTestAssignment = await _uow.TestAssignmentRepository
            .FindOneAsync(x => x.UserId == user.Id && x.TestId == test.Id);

        if (existingTestAssignment is not null)
        {
            throw new EntityConflictException("Test assignment for this user and test already exists");
        }

        var testAssignment = new TestAssignment()
        {
            UserId = user.Id,
            TestId = test.Id
        };

        _uow.TestAssignmentRepository.Add(testAssignment);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
