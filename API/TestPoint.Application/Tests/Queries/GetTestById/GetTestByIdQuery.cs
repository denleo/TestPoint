using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestById;

public class GetTestByIdQuery : IRequest<Test>
{
    public Guid TestId { get; set; }
    public Guid LoginId { get; set; }
    public LoginType Role { get; set; }
}
