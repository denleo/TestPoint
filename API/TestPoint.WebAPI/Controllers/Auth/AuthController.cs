using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.Domain;
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

    [HttpPost("auth/user")]
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

        return _jwtService.CreateToken(CreateClaims(loginResponse.UserId, loginResponse.Username, LoginType.User));
    }

    [HttpPost("auth/admin")]
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

        return _jwtService.CreateToken(CreateClaims(loginResponse.AdminId, loginResponse.Username, LoginType.Administrator));
    }

    private static IEnumerable<Claim> CreateClaims(int id, string username, LoginType loginType) => new List<Claim>
    {
        new(ClaimTypes.Sid, id.ToString()),
        new(ClaimTypes.Name, username),
        new(ClaimTypes.Role, loginType.ToString())
    };
}