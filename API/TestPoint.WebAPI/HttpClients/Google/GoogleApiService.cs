using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace TestPoint.WebAPI.HttpClients.Google;

public class GoogleApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoogleApiService> _logger;
    private readonly GoogleApiSettings _apiSettings;

    public GoogleApiService(HttpClient httpClient, IOptions<GoogleApiSettings> options, ILogger<GoogleApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiSettings = options.Value;
    }

    public async Task<GoogleAccountModel?> GetUserInfoAsync(string googleToken)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _apiSettings.UserInfo)
        {
            Headers =
            {
                { HeaderNames.Authorization, $"Bearer {googleToken}" },
            }
        };

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning($"Failed to get google user account information. {response.ReasonPhrase}");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleAccountModel>(content);
    }

    public async Task<byte[]?> FetchAvatarAsync(string uri)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await _httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning($"Failed to fetch google account avatar. {response.ReasonPhrase}");
            return null;
        }

        var stream = response.Content.ReadAsStream();
        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        var imageData = ms.ToArray();

        return imageData;
    }
}
