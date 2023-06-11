using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Queries.CheckGoogleUserLogin;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.Domain;
using TestPoint.WebAPI.HttpClients.Google;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
using TestPoint.WebAPI.Models.Admin;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Auth;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IJwtService _jwtService;
    private readonly GoogleApiService _googleApi;

    public AuthController(IJwtService jwtService, GoogleApiService googleApiService)
    {
        _jwtService = jwtService;
        _googleApi = googleApiService;
    }

    [SwaggerOperation(Summary = "User login action")]
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

    [SwaggerOperation(Summary = "User google sign in action")]
    [HttpPost("auth/google/user")]
    public async Task<ActionResult<string>> UserGoogleSignIn([FromBody] string googleToken)
    {
        var googleUserInfo = await _googleApi.GetUserInfoAsync(googleToken);

        if (googleUserInfo is not null)
        {
            var testPointUser = await Mediator.Send(new CheckGoogleUserLoginQuery(googleUserInfo.sub));

            if (testPointUser is not null)
            {
                return _jwtService.CreateToken(CreateClaims(testPointUser.UserId, testPointUser.Username, LoginType.User));
            }
        }

        return Unauthorized(new ErrorResult(HttpStatusCode.Unauthorized, "Google authorization failed."));
    }

    [SwaggerOperation(Summary = "Admin login action")]
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

    private static IEnumerable<Claim> CreateClaims(Guid id, string username, LoginType loginType) => new List<Claim>
    {
        new(ClaimTypes.Sid, id.ToString()),
        new(ClaimTypes.Name, username),
        new(ClaimTypes.Role, loginType.ToString())
    };
}