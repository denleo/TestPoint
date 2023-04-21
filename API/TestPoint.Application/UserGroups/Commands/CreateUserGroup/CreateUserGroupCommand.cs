using MediatR;

namespace TestPoint.Application.UserGroups.Commands.CreateUserGroup;

public class CreateUserGroupCommand : IRequest<CreateUserGroupResponse>
{
    public Guid AdministratorId { get; set; }
    public string GroupName { get; set; }
}
