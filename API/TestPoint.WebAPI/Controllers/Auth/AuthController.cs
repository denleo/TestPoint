using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestPoint.Application.Admins.Queries.CheckAdminLogin;
using TestPoint.Application.Users.Queries.CheckUserLogin;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Auth;

public class AuthController : BaseController
{
    private readonly IConfiguration _configuration;
    private const int UserTokenExpiration = 2;
    private const int AdminTokenExpiration = 3;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("auth/user/token")]
    public async Task<ActionResult<string>> UserLogin([FromBody] LoginDto login)
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

        return CreateAccessToken(CreateUserClaims(loginResponse), UserTokenExpiration);
    }

    [HttpPost("auth/admin/token")]
    public async Task<ActionResult<string>> AdminLogin([FromBody] LoginDto login)
    {
        var checkAdminLoginQuery = new CheckAdminLoginQuery
        {
            Username = login.Login,
            Password = login.Password
        };

        var loginResponse = await Mediator.Send(checkAdminLoginQuery);

        if (loginResponse is null)
        {
            return Unauthorized(new { Status = StatusCodes.Status401Unauthorized, Error = "Incorrect username or password" });
        }

        return CreateAccessToken(CreateAdminClaims(loginResponse), AdminTokenExpiration);
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

    private string CreateAccessToken(IEnumerable<Claim> claims, int duration)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:TokenSecurityKey").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(duration),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}