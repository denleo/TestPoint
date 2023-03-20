using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TestPoint.Application.Admins.Commands.CreateAdmin;
using TestPoint.Application.Admins.Commands.ResetAdminPassword;
using TestPoint.Application.Admins.Queries.GetCurrentAdmin;
using TestPoint.WebAPI.Filters;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Membership;

public class AdminController : BaseController
{
    private readonly IMemoryCache Cache;

    public AdminController(IMemoryCache memoryCache) => Cache = memoryCache;

    /// <summary>
    /// Create new administrator with temporary password
    /// </summary>
    /// <param name="newAdmin">New admin username</param>
    /// <returns>New admin id and password</returns>
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

    /// <summary>
    /// Reset password for administrator
    /// </summary>
    /// <param name="admin">Admin username</param>
    /// <returns>New generated password</returns>
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

    /// <summary>
    /// Get current administrator data
    /// </summary>
    /// <returns>Administrator data</returns>
    [HttpGet("session/admin"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<GetCurrentAdminResponse>> GetCurrentAdmin()
    {
        GetCurrentAdminResponse adminData;

        if (Cache.TryGetValue(LoginId!.Value, out adminData))
        {
            return adminData;
        }

        var getCurrentAdminQuery = new GetCurrentAdminQuery
        {
            AdminId = LoginId!.Value
        };

        adminData = await Mediator.Send(getCurrentAdminQuery);

        Cache.Set(LoginId!.Value, adminData, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(30)
        });

        return adminData;
    }
}