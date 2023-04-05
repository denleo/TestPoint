using Core.Models.Api;

namespace Core.Services.Api;

public interface ILogsApiHandler
{
    /// <exception cref="HttpRequestException">
    /// Thrown when failed to establish connection.
    /// </exception>
    Task<ResponseBag<string[]>> GetLogFileNames();

    /// <exception cref="HttpRequestException">
    /// Thrown when failed to establish connection.
    /// </exception>
    Task<ResponseBag<byte[]>> GetLogFile(string fileName);
}
