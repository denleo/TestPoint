using FluentValidation;
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
        ErrorResult errorResult;

        switch (exception)
        {
            case ValidationException fluentValidationException:

                var validationErrors = fluentValidationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .Select(x => new ValidationError()
                    {
                        Field = x.Key,
                        Errors = x.Select(x => x.ErrorMessage).ToArray(),
                    })
                    .ToArray();

                errorResult = new ValidationErrorResult(validationErrors);
                break;

            case RepositoryException:
                errorResult = new ErrorResult(HttpStatusCode.InternalServerError, "Internal server error");
                break;

            case BadEntityException:
                errorResult = new ErrorResult(HttpStatusCode.BadRequest, exception.Message);
                break;

            case EntityConflictException:
                errorResult = new ErrorResult(HttpStatusCode.Conflict, exception.Message);
                break;

            case EntityNotFoundException:
                errorResult = new ErrorResult(HttpStatusCode.NotFound, exception.Message);
                break;

            case ActionNotAllowedException:
                errorResult = new ErrorResult(HttpStatusCode.Forbidden, exception.Message);
                break;

            default:
                var logger = context.RequestServices.GetRequiredService<ILogger<CustomExceptionHandlerMiddleware>>();
                logger.LogError(exception, exception!.Message);

                errorResult = new ErrorResult(HttpStatusCode.InternalServerError, "Internal server error");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorResult.Status;
        return context.Response.WriteAsync(errorResult.ToJson());
    }
}