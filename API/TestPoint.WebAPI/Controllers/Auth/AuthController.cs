using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Commands.CreateUser;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.Application.Users.Queries.GetUserByEmail;
using TestPoint.Domain;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
using TestPoint.WebAPI.Models.Admin;
using TestPoint.WebAPI.Models.Google;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Auth;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IJwtService _jwtService;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IJwtService jwtService, IHttpClientFactory httpClientFactory)
    {
        _jwtService = jwtService;
        _httpClientFactory = httpClientFactory;
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
    public async Task<ActionResult<string>> UserGoogleSignIn(string googleToken)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v3/userinfo")
        {
            Headers =
            {
                { HeaderNames.Authorization, $"Bearer {googleToken}" },
            }
        };

        using var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var userModel = JsonConvert.DeserializeObject<GoogleAccountModel>(content);

            var existingUser = await Mediator.Send(new GetUserByEmailQuery(userModel.email));

            if (existingUser is null)
            {
                var createUserCommand = new CreateUserCommand
                {
                    IsGoogleAccount = true,
                    Email = userModel.email,
                    FirstName = userModel.given_name,
                    LastName = userModel.family_name
                };

                existingUser = await Mediator.Send(createUserCommand);
            }

            return _jwtService.CreateToken(CreateClaims(existingUser.Id, existingUser.Login.Username, LoginType.User));
        }

        return Unauthorized(new ErrorResult(HttpStatusCode.Unauthorized, "Google account can't be accessed."));
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