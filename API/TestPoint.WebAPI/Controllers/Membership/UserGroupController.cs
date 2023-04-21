using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestPoint.Application.UserGroups.Commands.AddUserToGroup;
using TestPoint.Application.UserGroups.Commands.CreateUserGroup;
using TestPoint.Application.UserGroups.Commands.DeleteUserGroup;
using TestPoint.Application.UserGroups.Commands.RemoveUserFromGroup;
using TestPoint.WebAPI.Models.UserGroup;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserGroupController : BaseController
{
    [SwaggerOperation(Summary = "Create a new user group (roles:admin)")]
    [HttpPost("usergroup"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CreateUserGroupResponse>> CreateUserGroup([FromBody] UserGroupDto newUserGroup)
    {
        var createUserGroupCommand = new CreateUserGroupCommand
        {
            AdministratorId = LoginId!.Value,
            GroupName = newUserGroup.GroupName
        };

        var response = await Mediator.Send(createUserGroupCommand);
        return response;
    }

    [SwaggerOperation(Summary = "Delete existing user group (roles:admin)")]
    [HttpDelete("usergroup/{id:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUserGroup(Guid id)
    {
        var deleteUserGroupCommand = new DeleteUserGroupCommand
        {
            GroupId = id
        };

        await Mediator.Send(deleteUserGroupCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Add user to the group (roles:admin)")]
    [HttpPost("usergroup/{groupId:guid}/users"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddUserToGroup([FromRoute] Guid groupId, [FromBody] Guid userId)
    {
        var addUserToGroupCommand = new AddUserToGroupCommand
        {
            GroupId = groupId,
            UserId = userId
        };

        await Mediator.Send(addUserToGroupCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Remove user from the group (roles:admin)")]
    [HttpDelete("usergroup/{groupId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> RemoveUserFromGroup([FromRoute] Guid groupId, [FromRoute] Guid userId)
    {
        var removeUserFromGroupCommand = new RemoveUserFromGroupCommand
        {
            GroupId = groupId,
            UserId = userId
        };

        await Mediator.Send(removeUserFromGroupCommand);
        return Ok();
    }
}
