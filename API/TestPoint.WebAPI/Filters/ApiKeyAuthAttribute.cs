using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace TestPoint.WebAPI.Filters;

public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
{
    private const string HeaderName = "api-key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var key))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var acceptableKeys = configuration.GetSection("ApiKeys").Get<string[]>();

        if (!acceptableKeys.Contains(key.First()))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var claims = new Claim[]
        {
            new (ClaimTypes.AuthenticationMethod, "ApiKey"),
            new (ClaimTypes.Role, "TrustedApplication")
        };
        context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
    }
}