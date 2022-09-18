using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestPoint.WebAPI.Controllers;

[ApiController]
[Route("api")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}