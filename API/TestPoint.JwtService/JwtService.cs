using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.JwtService;

public class JwtService : IJwtService
{
    private readonly JwtAuthSettings _jwtSettings;

    public JwtService(IOptions<JwtAuthSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(IEnumerable<Claim> claims, bool isShortToken = false)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.TokenSecurityKey));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(isShortToken ? _jwtSettings.ShortTokenExp : _jwtSettings.TokenExp));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public List<Claim> ParseToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.TokenSecurityKey)),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = _jwtSettings.Audience,
            ValidIssuer = _jwtSettings.Issuer
        };

        tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        return ((JwtSecurityToken)validatedToken).Claims.ToList();
    }
}