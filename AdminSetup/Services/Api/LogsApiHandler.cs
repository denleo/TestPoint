using Core.Models.Api;
using Core.Services.Api;
using Core.Services.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;

namespace Services.Api;

public class LogsApiHandler : ILogsApiHandler
{
    private readonly string GetLogFileNamesEndpoint, GetLogFileEndpoint;
    private readonly IHttpService HttpService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public LogsApiHandler(IConfiguration configuration, IHttpService httpService)
    {
        GetLogFileNamesEndpoint = configuration.GetSection("AppSettings:Endpoints:GetLogFileNames").Value;
        GetLogFileEndpoint = configuration.GetSection("AppSettings:Endpoints:GetLogFile").Value;
        HttpService = httpService;
    }

    public async Task<ResponseBag<string[]>> GetLogFileNames()
    {
        var httpClient = HttpService.GetHttpClient();

        var response = await httpClient.GetAsync(GetLogFileNamesEndpoint);

        string[]? body = null;
        if (response!.StatusCode == HttpStatusCode.OK)
        {
            var stringBody = await response.Content.ReadAsStringAsync();
            body = JsonSerializer.Deserialize<string[]>(stringBody, JsonOptions);
        }

        return new ResponseBag<string[]>
        {
            Body = body,
            Code = response.StatusCode
        };
    }

    public async Task<ResponseBag<byte[]>> GetLogFile(string fileName)
    {
        var httpClient = HttpService.GetHttpClient();

        var endpoint = GetLogFileEndpoint + $"?file={fileName}";
        var fileBytes = await httpClient.GetByteArrayAsync(endpoint);

        return new ResponseBag<byte[]>
        {
            Body = fileBytes,
            Code = HttpStatusCode.OK
        };
    }
}