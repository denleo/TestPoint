using MediatR;

namespace TestPoint.Application.Tests.Queries.GetTestsByUser;

public class GetTestsByUserQuery : IRequest<List<TestInformation>>
{
    public Guid UserId { get; set; }
    public UserTestsFilter Filter { get; set; }
}
