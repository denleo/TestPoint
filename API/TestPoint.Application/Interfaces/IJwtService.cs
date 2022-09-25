using System.Security.Claims;

namespace TestPoint.Application.Interfaces;

public interface IJwtService
{
    string GetToken(IEnumerable<Claim> claims);
}