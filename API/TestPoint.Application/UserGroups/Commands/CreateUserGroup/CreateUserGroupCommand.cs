using MediatR;

namespace TestPoint.Application.UserGroups.Commands.CreateUserGroup;

public class CreateUserGroupCommand : IRequest<UserGroupInformation>
{
    public Guid AdministratorId { get; set; }
    public string GroupName { get; set; }
}
