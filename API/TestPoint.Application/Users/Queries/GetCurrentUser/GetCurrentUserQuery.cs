using MediatR;

namespace TestPoint.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
{
    public int UserId { get; set; }
}