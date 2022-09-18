using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.Users.Commands.CreateUser;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers;

public class UserController : BaseController
{
    [HttpPost("users")]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserDto newUser)
    {
        var createUserCommand = new CreateUserCommand()
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
}