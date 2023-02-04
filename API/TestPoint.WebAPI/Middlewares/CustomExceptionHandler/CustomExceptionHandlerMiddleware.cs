using System.Net;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception? exception)
        {
            await HandleExceptions(context, exception);
        }
    }

    private Task HandleExceptions(HttpContext context, Exception? exception)
    {
        var status = HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case EntityConflictException:
                status = HttpStatusCode.Conflict;
                break;

            case EntityNotFoundException:
                status = HttpStatusCode.NotFound;
                break;

            case ActionNotAllowedException:
                status = HttpStatusCode.Forbidden;
                break;

            default:
                var log = context.RequestServices.GetRequiredService<ILogService>();
                log.Log<CustomExceptionHandlerMiddleware>(LogLevel.Error, exception.Message, exception);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(new ErrorResult(status, exception.Message).ToJson());
    }
}