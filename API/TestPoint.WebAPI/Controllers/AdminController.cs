using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.Admins.Commands.CreateAdmin;
using TestPoint.Application.Admins.Commands.ResetAdminPassword;
using TestPoint.WebAPI.Filters;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers;

public class AdminController : BaseController
{
    [HttpPost("setup/admins"), ApiKeyAuth]
    public async Task<ActionResult<CreateAdminResponse>> CreateAdmin([FromBody] CreateAdminDto newAdmin)
    {
        var createAdminCommand = new CreateAdminCommand
        {
            Username = newAdmin.Username
        };

        var response = await Mediator.Send(createAdminCommand);
        return response;
    }

    [HttpPatch("setup/admin/password"), ApiKeyAuth]
    public async Task<ActionResult<ResetAdminPasswordResponse>> ResetAdminPassword([FromBody] CreateAdminDto admin)
    {
        var resetAdminPasswordCommand = new ResetAdminPasswordCommand
        {
            Username = admin.Username
        };

        var response = await Mediator.Send(resetAdminPasswordCommand);
        return response;
    }
}