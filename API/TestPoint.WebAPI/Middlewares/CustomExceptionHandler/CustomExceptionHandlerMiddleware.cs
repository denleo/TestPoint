﻿using System.Net;
using System.Text.Json;
using TestPoint.Application.Common.Exceptions;

namespace TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

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
        catch (Exception exception)
        {
            await HandleExceptions(context, exception);
        }
    }

    private Task HandleExceptions(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case EntityExistsException existsException:
                code = HttpStatusCode.Conflict;
                break;

            case EntityNotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new { Status = (int)code, Error = exception.Message }, JsonOptions);
        }

        //TODO logging
        return context.Response.WriteAsync(result);
    }
}