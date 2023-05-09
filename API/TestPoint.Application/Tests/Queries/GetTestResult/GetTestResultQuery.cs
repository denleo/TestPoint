using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestResult;

public class GetTestResultQuery : IRequest<TestCompletion>
{
    public Guid UserId { get; set; }
    public Guid TestId { get; set; }
}
