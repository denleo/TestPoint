using MediatR;

namespace TestPoint.Application.UserGroups.Commands.AddUserToGroup;

public class AddUserToGroupCommand : IRequest
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}
