using MediatR;
using TestPoint.Application.Users;

namespace TestPoint.Application.UserGroups.Queries.GetUsersInGroup;

public class GetUsersInGroupQuery : IRequest<List<UserInformation>>
{
    public Guid GroupId { get; set; }
}
