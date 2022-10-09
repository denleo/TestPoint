using Core.Models.Api;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Services.Api;

internal class RequestHandler
{
    private readonly string _apiKey;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RequestHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    /// <exception cref="HttpRequestException">
    /// Thrown when failed to establish connection.
    /// </exception>
    public async Task<ResponseBag<TResponse>> SendRequest<TRequest, TResponse>(string endPoint, RequestMethod method, TRequest? requestBody)
        where TResponse : class
        where TRequest : class
    {
        StringContent? requestContent = null;
        if (method != RequestMethod.DELETE && method != RequestMethod.GET)
        {
            var serialized = JsonSerializer.Serialize(requestBody, JsonOptions);
            requestContent = new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

        var response = method switch
        {
            RequestMethod.POST => await httpClient.PostAsync(endPoint, requestContent),
            RequestMethod.PATCH => await httpClient.PatchAsync(endPoint, requestContent),
            RequestMethod.PUT => await httpClient.PutAsync(endPoint, requestContent),
            RequestMethod.GET => await httpClient.GetAsync(endPoint),
            RequestMethod.DELETE => await httpClient.DeleteAsync(endPoint),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, "Not supported method")
        };

        TResponse? body = null;
        if (response!.StatusCode == HttpStatusCode.OK)
        {
            var stringBody = await response.Content.ReadAsStringAsync();
            body = JsonSerializer.Deserialize<TResponse>(stringBody, JsonOptions);
        }

        return new ResponseBag<TResponse>
        {
            Body = body,
            Code = response.StatusCode
        };
    }
}