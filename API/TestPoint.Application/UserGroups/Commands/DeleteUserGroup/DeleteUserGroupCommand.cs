using MediatR;

namespace TestPoint.Application.UserGroups.Commands.DeleteUserGroup;

public class DeleteUserGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
}
