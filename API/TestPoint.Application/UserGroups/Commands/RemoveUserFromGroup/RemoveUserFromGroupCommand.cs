using MediatR;

namespace TestPoint.Application.UserGroups.Commands.RemoveUserFromGroup;

public class RemoveUserFromGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}
