using Core.Services.Http;
using Microsoft.Extensions.Configuration;

namespace Services.HttpService;

public class HttpService : IHttpService
{
    private readonly HttpClient HttpClient;

    public HttpService(IConfiguration config)
    {
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            };

        HttpClient = new HttpClient(handler);
        HttpClient.DefaultRequestHeaders.Add("api-key", config.GetSection("AppSettings:Key").Value);
    }

    public HttpClient GetHttpClient() => HttpClient;

    public void Dispose()
    {
        HttpClient?.Dispose();
    }
}
