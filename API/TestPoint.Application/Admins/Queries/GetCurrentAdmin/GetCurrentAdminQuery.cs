using MediatR;

namespace TestPoint.Application.Admins.Queries.GetCurrentAdmin;

public class GetCurrentAdminQuery : IRequest<GetCurrentAdminResponse>
{
    public int AdminId { get; set; }
}