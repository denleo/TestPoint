using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Application.Common.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(IEnumerable<Claim> claims, bool isEmailToken = false)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:TokenSecurityKey").Value));

        var tokenExpiration = int.Parse(isEmailToken ?
            _configuration.GetSection("Jwt:EmailTokenExp").Value :
            _configuration.GetSection("Jwt:TokenExp").Value);

        //var issuer = _configuration.GetSection("Jwt:Issuer").Value;
        //var audience = _configuration.GetSection("Jwt:Audience").Value;

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(tokenExpiration),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public List<Claim> ParseToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("Jwt:TokenSecurityKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        return ((JwtSecurityToken)validatedToken).Claims.ToList();
    }
}