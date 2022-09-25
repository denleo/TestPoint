using Core.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Core;

public class RequestHandler
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
    public async Task<ResponseBag<TResponse>> SendRequest<TRequest, TResponse>(string endPoint, RequestMethod method, TRequest? request)
        where TResponse : class
        where TRequest : class
    {
        StringContent? requestBody = null;
        if (method != RequestMethod.DELETE && method != RequestMethod.GET)
        {
            var serialized = JsonSerializer.Serialize(request, JsonOptions);
            requestBody = new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

        HttpResponseMessage? response = null;

        switch (method)
        {
            case RequestMethod.POST:
                response = await httpClient.PostAsync(endPoint, requestBody);
                break;
            case RequestMethod.PATCH:
                response = await httpClient.PatchAsync(endPoint, requestBody);
                break;
            case RequestMethod.PUT:
                response = await httpClient.PutAsync(endPoint, requestBody);
                break;
            case RequestMethod.GET:
                response = await httpClient.GetAsync(endPoint);
                break;
            case RequestMethod.DELETE:
                response = await httpClient.DeleteAsync(endPoint);
                break;
        }

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