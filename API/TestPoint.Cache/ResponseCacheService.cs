using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Cache;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDistributedCache _distributedCache;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ResponseCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheResponseAsync(string key, object? response, TimeSpan liveTime)
    {
        if (response == null || key == null)
        {
            return;
        }

        var serializedResponse = JsonSerializer.Serialize(response, _jsonOptions);

        await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = liveTime
        });
    }

    public async Task<string?> GetResponseAsync(string key)
    {
        var cachedResponse = await _distributedCache.GetStringAsync(key);
        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }
}
