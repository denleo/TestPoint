using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Cache;

namespace TestPoint.WebAPI.Attributes;

public class RedisCacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly TimeSpan _liveTime;

    public RedisCacheAttribute(int liveTimeSec)
    {
        _liveTime = new TimeSpan(0, 0, liveTimeSec);
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

        if (!cacheSettings.Enabled)
        {
            await next();
            return;
        }

        var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
        var key = GetFullUrl(context.HttpContext.Request);

        var cachedResponse = await responseCacheService.GetResponseAsync(key);
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            var result = new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = cachedResponse
            };

            context.Result = result;
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult && objectResult.StatusCode == 200)
        {
            await responseCacheService.CacheResponseAsync(key, objectResult.Value, _liveTime);
        }
    }

    private static string GetFullUrl(HttpRequest request)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"{request.Path}?");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            stringBuilder.Append($"{key}={value}&");
        }

        stringBuilder.Length--;
        return stringBuilder.ToString();
    }
}
