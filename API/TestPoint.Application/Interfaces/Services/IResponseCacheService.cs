namespace TestPoint.Application.Interfaces.Services;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string key, object? response, TimeSpan liveTime);
    Task<string?> GetResponseAsync(string key);
}
