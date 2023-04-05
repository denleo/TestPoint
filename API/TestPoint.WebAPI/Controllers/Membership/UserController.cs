using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestPoint.Application.Users.Commands.ChangePassword;
using TestPoint.Application.Users.Commands.CreateUser;
using TestPoint.Application.Users.Queries.GetCurrentUser;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserController : BaseController
{
    [SwaggerOperation(Summary = "Create a new user")]
    [HttpPost("users"), AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] UserDto newUser)
    {
        var createUserCommand = new CreateUserCommand
        {
            Username = newUser.Username,
            Password = newUser.Password,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName
        };

        var response = await Mediator.Send(createUserCommand);
        return response;
    }

    [SwaggerOperation(Summary = "Get current user data (roles:user)")]
    [HttpGet("session/user"), Authorize(Roles = "User")]
    public async Task<ActionResult<GetCurrentUserResponse>> GetCurrentUser()
    {
        var getCurrentUserQuery = new GetCurrentUserQuery
        {
            UserId = LoginId!.Value
        };

        var userData = await Mediator.Send(getCurrentUserQuery);
        return userData;
    }

    [SwaggerOperation(Summary = "Change password for current user (roles:user)")]
    [HttpPatch("session/user/password"), Authorize(Roles = "User")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var changeUserPasswordCommand = new ChangeUserPasswordCommand
        {
            UserId = LoginId!.Value,
            OldPassword = changePasswordDto.OldPassword,
            NewPassword = changePasswordDto.NewPassword
        };

        await Mediator.Send(changeUserPasswordCommand);
        return Ok();
    }
}