using MediatR;

namespace TestPoint.Application.Tests.Queries.GetUsersOnTest;

public class GetUsersOnTestQuery : IRequest<List<UserOnTest>>
{
    public Guid TestId { get; set; }
}
