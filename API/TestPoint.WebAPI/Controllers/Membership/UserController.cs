using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.Users.Commands.CreateUser;
using TestPoint.Application.Users.Queries.GetCurrentUser;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserController : BaseController
{
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="newUser">New user data</param>
    /// <returns>New user id</returns>
    [AllowAnonymous]
    [HttpPost("users")]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserDto newUser)
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

    /// <summary>
    /// Get current user data
    /// </summary>
    /// <returns>User data</returns>
    [HttpGet("session/user"), Authorize(Roles = "User")]
    public async Task<ActionResult<GetCurrentUserResponse>> GetCurrentUser()
    {
        var getCurrentUserQuery = new GetCurrentUserQuery
        {
            UserId = LoginId!.Value
        };

        var response = await Mediator.Send(getCurrentUserQuery);
        return response;
    }
}