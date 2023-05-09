using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.TestAssignments.Commands.CreateTestAssignment;

public class CreateTestAssignmentCommand : IRequest
{
    public Guid TestId { get; set; }
    public Guid UserId { get; set; }
}
