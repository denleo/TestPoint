using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;
using TestPoint.WebAPI.Filters;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

namespace TestPoint.WebAPI.Controllers.Logs;

public class LogController : BaseController
{
    private readonly Regex _logFileRegex = new Regex("^log\\d{4}-\\d{2}-\\d{2}[.]json$");

    [SwaggerOperation(Summary = "Get existing log file names (role:tpa)")]
    [HttpGet("_setup/logs/names"), ApiKeyAuth]
    public IActionResult GetExistingLogFileNames()
    {
        var searchPattern = $"log*.json";
        var logFiles = Directory.GetFiles(AppContext.BaseDirectory, searchPattern);

        return Ok(logFiles.Select(filePath => Path.GetFileName(filePath)));
    }

    [SwaggerOperation(Summary = "Get app log file (role:tpa)")]
    [HttpGet("_setup/logs"), ApiKeyAuth]
    public async Task<IActionResult> GetLogFile([FromQuery] string file)
    {
        if (!_logFileRegex.IsMatch(file))
        {
            return BadRequest(new ErrorResult(System.Net.HttpStatusCode.NotFound, "Invalid file parameter pattern."));
        }

        var searchResult = Directory.GetFiles(AppContext.BaseDirectory, file);

        if (searchResult.Length == 0)
        {
            return NotFound(new ErrorResult(System.Net.HttpStatusCode.NotFound, "File was not found."));
        }

        using var fs = new FileStream(searchResult[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var ms = new MemoryStream();
        await fs.CopyToAsync(ms);
        var content = ms.ToArray();

        if (content.Length == 0)
        {
            return NoContent();
        }

        return File(content, "application/json", file);
    }
}
