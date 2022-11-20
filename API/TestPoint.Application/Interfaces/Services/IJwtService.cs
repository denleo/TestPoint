using System.Security.Claims;

namespace TestPoint.Application.Interfaces.Services;

public interface IJwtService
{
    string CreateToken(IEnumerable<Claim> claims, bool isEmailToken = false);
    List<Claim> ParseToken(string token);
}