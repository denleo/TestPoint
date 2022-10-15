using System.Security.Claims;

namespace TestPoint.Application.Interfaces.Services;

public interface IJwtService
{
    string GetToken(IEnumerable<Claim> claims);
}