using System.Net;

namespace Core.Models.Api;

public class ResponseBag<T> where T : class
{
    public HttpStatusCode Code { get; set; }

    public T? Body { get; set; }
}