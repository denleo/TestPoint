using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.SubmitTestResult;

public class SubmitTestResultCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid TestId { get; set; }
    public TestCompletion TestCompletion { get; set; }
}
