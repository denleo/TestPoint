using MediatR;

namespace TestPoint.Application.TestAssignments.Commands.DeleteTestAssignment;

public class DeleteTestAssignmentCommand : IRequest
{
    public Guid TestId { get; set; }
    public Guid UserId { get; set; }
}
