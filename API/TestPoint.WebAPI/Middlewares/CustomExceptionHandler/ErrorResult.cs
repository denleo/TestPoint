using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

public class ErrorResult
{
    [JsonPropertyOrder(0)]
    public HttpStatusCode Status { get; }

    [JsonPropertyOrder(1)]
    public string Message { get; }

    [NonSerialized]
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ErrorResult(HttpStatusCode statusCode, string errorMessage)
    {
        Status = statusCode;
        Message = errorMessage;
    }

    public virtual string ToJson() => JsonSerializer.Serialize(this, JsonOptions);
}
