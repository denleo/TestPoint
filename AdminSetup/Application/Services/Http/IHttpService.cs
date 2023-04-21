namespace Core.Services.Http;

public interface IHttpService : IDisposable
{
    HttpClient GetHttpClient();
}
