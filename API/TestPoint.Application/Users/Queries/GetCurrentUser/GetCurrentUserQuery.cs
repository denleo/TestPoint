using MediatR;

namespace TestPoint.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
{
    public Guid UserId { get; set; }
}