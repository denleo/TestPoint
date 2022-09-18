using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.Admins.Commands.CreateAdmin;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers;

public class AdminController : BaseController
{
    [HttpPost("admins")]
    public async Task<ActionResult<CreateAdminResponse>> CreateAdmin([FromBody] CreateAdminDto newAdmin)
    {
        var createAdminCommand = new CreateAdminCommand()
        {
            Username = newAdmin.Username,
            Password = newAdmin.Password
        };

        var response = await Mediator.Send(createAdminCommand);
        return response;
    }
}