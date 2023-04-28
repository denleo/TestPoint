using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestPoint.Application.UserGroups;
using TestPoint.Application.UserGroups.Commands.AddUserToGroup;
using TestPoint.Application.UserGroups.Commands.CreateUserGroup;
using TestPoint.Application.UserGroups.Commands.DeleteUserGroup;
using TestPoint.Application.UserGroups.Commands.RemoveUserFromGroup;
using TestPoint.Application.UserGroups.Queries.GetUserGroups;
using TestPoint.Application.UserGroups.Queries.GetUsersInGroup;
using TestPoint.Application.Users;
using TestPoint.WebAPI.Models.UserGroup;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserGroupController : BaseController
{
    [SwaggerOperation(Summary = "Create a new user group (roles:admin)")]
    [HttpPost("usergroups"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserGroupInformation>> CreateUserGroup([FromBody] UserGroupDto newUserGroup)
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
    [HttpDelete("usergroups/{id:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUserGroup(Guid id)
    {
        var deleteUserGroupCommand = new DeleteUserGroupCommand
        {
            GroupId = id
        };

        await Mediator.Send(deleteUserGroupCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Get all existing user groups (roles:admin)")]
    [HttpGet("usergroups"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<UserGroupInformation>>> GetAllUserGroups()
    {
        var getUserGroupsQuery = new GetUserGroupsQuery
        {
            AdminId = LoginId!.Value,
        };

        var userGroups = await Mediator.Send(getUserGroupsQuery);

        if (userGroups.Count == 0)
        {
            return NoContent();
        }

        return userGroups;
    }

    [SwaggerOperation(Summary = "Add user to the group (roles:admin)")]
    [HttpPost("usergroups/{groupId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddUserToGroup([FromRoute] Guid groupId, [FromRoute] Guid userId)
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
    [HttpDelete("usergroups/{groupId:guid}/users/{userId:guid}"), Authorize(Roles = "Administrator")]
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

    [SwaggerOperation(Summary = "Get users from the group (roles:admin)")]
    [HttpGet("usergroups/{groupId:guid}/users"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<UserInformation>>> GetUsersInGroup([FromRoute] Guid groupId)
    {
        var getUsersInGroupQuery = new GetUsersInGroupQuery
        {
            GroupId = groupId
        };

        var users = await Mediator.Send(getUsersInGroupQuery);

        if (users.Count == 0)
        {
            return NoContent();
        }

        return users;
    }
}
