using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestPoint.Application.Admins.Commands.ChangePassword;
using TestPoint.Application.Admins.Commands.CreateAdmin;
using TestPoint.Application.Admins.Commands.ResetAdminPassword;
using TestPoint.Application.Admins.Queries.GetCurrentAdmin;
using TestPoint.WebAPI.Attributes;
using TestPoint.WebAPI.Models.Admin;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Membership;

public class AdminController : BaseController
{
    [SwaggerOperation(Summary = "Create new administrator with temporary password (roles:tpa)")]
    [HttpPost("_setup/admins"), ApiKeyAuth]
    public async Task<ActionResult<CreateAdminResponse>> CreateAdmin([FromBody] AdminDto newAdmin)
    {
        var createAdminCommand = new CreateAdminCommand
        {
            Username = newAdmin.Username
        };

        var response = await Mediator.Send(createAdminCommand);
        return response;
    }

    [SwaggerOperation(Summary = "Reset password for administrator (roles:tpa)")]
    [HttpPatch("_setup/admin/password"), ApiKeyAuth]
    public async Task<ActionResult<ResetAdminPasswordResponse>> ResetAdminPassword([FromBody] AdminDto admin)
    {
        var resetAdminPasswordCommand = new ResetAdminPasswordCommand
        {
            Username = admin.Username
        };

        var response = await Mediator.Send(resetAdminPasswordCommand);
        return response;
    }

    [SwaggerOperation(Summary = "Get current administrator data (roles:admin)")]
    [HttpGet("session/admin"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<GetCurrentAdminResponse>> GetCurrentAdmin()
    {
        var getCurrentAdminQuery = new GetCurrentAdminQuery
        {
            AdminId = LoginId!.Value
        };

        var adminData = await Mediator.Send(getCurrentAdminQuery);
        return adminData;
    }

    [SwaggerOperation(Summary = "Change password for current admin (roles:admin)")]
    [HttpPatch("session/admin/password"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var changeAdminPasswordCommand = new ChangeAdminPasswordCommand
        {
            AdminId = LoginId!.Value,
            OldPassword = changePasswordDto.OldPassword,
            NewPassword = changePasswordDto.NewPassword
        };

        await Mediator.Send(changeAdminPasswordCommand);
        return Ok();
    }
}