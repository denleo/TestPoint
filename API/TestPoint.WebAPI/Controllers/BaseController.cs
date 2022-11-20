using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestPoint.Domain;

namespace TestPoint.WebAPI.Controllers;

[ApiController]
[Route("api")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected int? LoginId
    {
        get
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if (id != null)
            {
                return int.Parse(id);
            }

            return null;
        }
    }

    protected string? LoginUsername => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

    protected LoginType? LoginRole
    {
        get
        {
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (role != null)
            {
                return Enum.Parse<LoginType>(role);
            }

            return null;
        }
    }
}