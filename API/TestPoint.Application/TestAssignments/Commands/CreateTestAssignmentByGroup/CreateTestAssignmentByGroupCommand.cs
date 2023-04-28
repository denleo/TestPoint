using MediatR;

namespace TestPoint.Application.TestAssignments.Commands.CreateTestAssignmentByGroup;

public class CreateTestAssignmentByGroupCommand : IRequest
{
    public Guid TestId { get; set; }
    public Guid UserGroupId { get; set; }
}
