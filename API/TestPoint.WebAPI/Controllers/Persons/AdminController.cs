using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPoint.Application.Admins.Commands.CreateAdmin;
using TestPoint.Application.Admins.Commands.ResetAdminPassword;
using TestPoint.Application.Admins.Queries.GetCurrentAdmin;
using TestPoint.WebAPI.Filters;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Persons;

public class AdminController : BaseController
{
    [HttpPost("_setup/admins"), ApiKeyAuth]
    public async Task<ActionResult<CreateAdminResponse>> CreateAdmin([FromBody] CreateAdminDto newAdmin)
    {
        var createAdminCommand = new CreateAdminCommand
        {
            Username = newAdmin.Username
        };

        var response = await Mediator.Send(createAdminCommand);
        return response;
    }

    [HttpPatch("_setup/admin/password"), ApiKeyAuth]
    public async Task<ActionResult<ResetAdminPasswordResponse>> ResetAdminPassword([FromBody] CreateAdminDto admin)
    {
        var resetAdminPasswordCommand = new ResetAdminPasswordCommand
        {
            Username = admin.Username
        };

        var response = await Mediator.Send(resetAdminPasswordCommand);
        return response;
    }

    [HttpGet("session/admin"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<GetCurrentAdminResponse>> GetCurrentAdmin()
    {
        var getCurrentAdminQuery = new GetCurrentAdminQuery
        {
            AdminId = LoginId!.Value
        };

        var response = await Mediator.Send(getCurrentAdminQuery);
        return response;
    }
}