using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.TestAssignments.Commands.CreateTestAssignmentByGroup;

public class CreateTestAssignmentByGroupHandler : IRequestHandler<CreateTestAssignmentByGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateTestAssignmentByGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(CreateTestAssignmentByGroupCommand request, CancellationToken cancellationToken)
    {
        var userGroup = await _uow.UserGroupRepository.FindOneAsync(userGroup => userGroup.Id == request.UserGroupId);

        if (userGroup is null)
        {
            throw new EntityNotFoundException($"User group with {request.UserGroupId} id was not found");
        }

        var test = await _uow.TestRepository.FindOneAsync(test => test.Id == request.TestId);

        if (test is null)
        {
            throw new EntityNotFoundException($"Test with {request.TestId} id was not found");
        }

        var usersOnTest = (await _uow.TestAssignmentRepository
            .FilterByAsync(testAssignment => testAssignment.TestId == test.Id))
            .Select(assignment => assignment.UserId);

        var usersInGroup = userGroup.Users.Select(x => x.Id);

        var usersToAdd = usersInGroup.Except(usersOnTest);

        if (usersToAdd.Any())
        {
            foreach (var userId in usersToAdd)
            {
                var testAssignment = new TestAssignment()
                {
                    TestId = test.Id,
                    UserId = userId
                };

                _uow.TestAssignmentRepository.Add(testAssignment);
            }

            await _uow.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
