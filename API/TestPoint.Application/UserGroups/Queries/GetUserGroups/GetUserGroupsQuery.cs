using MediatR;

namespace TestPoint.Application.UserGroups.Queries.GetUserGroups;

public class GetUserGroupsQuery : IRequest<List<UserGroupInformation>>
{
    public Guid AdminId { get; set; }
}
