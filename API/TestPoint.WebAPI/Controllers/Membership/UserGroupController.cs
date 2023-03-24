using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.UserGroups.Commands.AddUserToGroup;
using TestPoint.Application.UserGroups.Commands.CreateUserGroup;
using TestPoint.Application.UserGroups.Commands.DeleteUserGroup;
using TestPoint.Application.UserGroups.Commands.RemoveUserFromGroup;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserGroupController : BaseController
{
    /// <summary>
    /// Create new user group
    /// </summary>
    /// <param name="newUserGroup">New user group data</param>
    /// <returns>New user group id</returns>
    [HttpPost("usergroup"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CreateUserGroupResponse>> CreateUserGroup([FromBody] CreateUserGroupDto newUserGroup)
    {
        var createUserGroupCommand = new CreateUserGroupCommand
        {
            AdministratorId = LoginId.Value,
            GroupName = newUserGroup.GroupName
        };

        var response = await Mediator.Send(createUserGroupCommand);
        return response;
    }

    /// <summary>
    /// Delete existing user group
    /// </summary>
    /// <param name="id">User group id</param>
    /// <returns></returns>
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

    /// <summary>
    /// Add user to the group
    /// </summary>
    /// <param name="groupId">Group id</param>
    /// <param name="userId">User id</param>
    /// <returns></returns>
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

    /// <summary>
    /// Remove user from the group
    /// </summary>
    /// <param name="groupId">Group id</param>
    /// <param name="userId">User id</param>
    /// <returns></returns>
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
