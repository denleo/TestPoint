using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestsByAdmin;

public class GetTestsByAdminQuery : IRequest<List<Test>>
{
    public Guid AdminId { get; set; }
}
