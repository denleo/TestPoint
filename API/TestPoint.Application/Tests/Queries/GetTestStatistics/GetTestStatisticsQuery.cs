using MediatR;

namespace TestPoint.Application.Tests.Queries.GetTestStatistics;

public class GetTestStatisticsQuery : IRequest<TestStatistics?>
{
    public Guid TestId { get; set; }
}
