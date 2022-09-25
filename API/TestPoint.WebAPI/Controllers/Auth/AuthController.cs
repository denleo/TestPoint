using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Interfaces;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Auth;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("auth/user/token")]
    public async Task<ActionResult<string>> UserLogin([FromBody] UserLoginDto login)
    {
        var checkUserLoginQuery = new CheckUserLoginQuery
        {
            Login = login.Login,
            Password = login.Password
        };

        var loginResponse = await Mediator.Send(checkUserLoginQuery);

        if (loginResponse is null)
        {
            return Unauthorized(new { Status = StatusCodes.Status401Unauthorized, Error = "Incorrect username or password" });
        }

        return _jwtService.GetToken(CreateUserClaims(loginResponse));
    }

    [HttpPost("auth/admin/token")]
    public async Task<ActionResult<string>> AdminLogin([FromBody] AdminLoginDto login)
    {
        var checkAdminLoginQuery = new CheckAdminLoginQuery
        {
            Username = login.Username,
            Password = login.Password
        };

        var loginResponse = await Mediator.Send(checkAdminLoginQuery);

        if (loginResponse is null)
        {
            return Unauthorized(new { Status = StatusCodes.Status401Unauthorized, Error = "Incorrect username or password" });
        }

        return _jwtService.GetToken(CreateAdminClaims(loginResponse));
    }

    private static IEnumerable<Claim> CreateUserClaims(CheckUserLoginResponse user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
    }

    private static IEnumerable<Claim> CreateAdminClaims(CheckAdminLoginResponse admin)
    {
        return new List<Claim>
        {
            new(ClaimTypes.Name, admin.Username),
            new(ClaimTypes.Role, admin.Role.ToString())
        };
    }
}