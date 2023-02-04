using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.Domain;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
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

    /// <summary>
    /// User login action
    /// </summary>
    /// <param name="login">User credentials</param>
    /// <returns>JWT Token</returns>
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
            return Unauthorized(new ErrorResult(HttpStatusCode.Unauthorized, "Incorrect username or password."));
        }

        return _jwtService.CreateToken(CreateClaims(loginResponse.UserId, loginResponse.Username, LoginType.User));
    }

    /// <summary>
    /// Admin login action
    /// </summary>
    /// <param name="login">Admin credentials</param>
    /// <returns>JWT Token</returns>
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
            return Unauthorized(new ErrorResult(HttpStatusCode.Unauthorized, "Incorrect username or password."));
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