using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

public class ValidationErrorResult : ErrorResult
{
    [JsonPropertyOrder(2)]
    public ValidationError[] ValidationResult { get; set; }

    public ValidationErrorResult(ValidationError[] validationErrors)
        : base(HttpStatusCode.BadRequest, "A validation problem occured")
    {
        ValidationResult = validationErrors;
    }

    public override string ToJson() => JsonSerializer.Serialize(this, JsonOptions);
}

public class ValidationError
{
    public string Field { get; set; }
    public string[] Errors { get; set; }
}
