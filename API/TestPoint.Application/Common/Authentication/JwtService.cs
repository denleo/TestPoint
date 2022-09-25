using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestPoint.Application.Interfaces;

namespace TestPoint.Application.Common.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:TokenSecurityKey").Value));

        var tokenExpiration = int.Parse(_configuration.GetSection("Jwt:TokenExp").Value);

        //var issuer = _configuration.GetSection("Jwt:Issuer").Value;
        //var audience = _configuration.GetSection("Jwt:Audience").Value;

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(tokenExpiration),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}