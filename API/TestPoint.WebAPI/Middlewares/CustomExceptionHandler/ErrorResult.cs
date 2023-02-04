using System.Net;
using System.Text.Json;

namespace TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

public sealed class ErrorResult
{
    [NonSerialized]
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #region Payload

    public HttpStatusCode Status { get; }
    public string Error { get; }

    #endregion

    public ErrorResult(HttpStatusCode statusCode, string errorMessage)
    {
        Status = statusCode;
        Error = errorMessage;
    }

    public string ToJson() => JsonSerializer.Serialize(this, JsonOptions);
}
