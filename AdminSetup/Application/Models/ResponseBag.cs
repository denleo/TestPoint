using System.Net;

namespace Core.Models;

public class ResponseBag<T> where T: class
{
    public HttpStatusCode Code { get; set; }

    public T? Body { get; set; }
}