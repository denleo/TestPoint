using MediatR;

namespace TestPoint.Application.Tests.Queries.GetTestsByAdmin;

public class GetTestsByAdminQuery : IRequest<List<TestInformation>>
{
    public Guid AdminId { get; set; }
}
