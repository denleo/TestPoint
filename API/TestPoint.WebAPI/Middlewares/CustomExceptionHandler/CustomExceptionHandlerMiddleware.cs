using System.Net;
using TestPoint.Application.Common.Exceptions;

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
            case BadEntityException:
                status = HttpStatusCode.BadRequest;
                break;

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
                var logger = context.RequestServices.GetRequiredService<ILogger<CustomExceptionHandlerMiddleware>>();
                logger.LogError(exception, exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        var errorResult = new ErrorResult(status, status == HttpStatusCode.InternalServerError ? "Internal server error" : exception.Message);
        return context.Response.WriteAsync(errorResult.ToJson());
    }
}