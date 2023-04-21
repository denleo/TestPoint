using Core.Models.Api;
using Core.Models.Api.CreateAdmin;
using Core.Models.Api.ResetAdminPassword;
using Core.Services.Api;
using Core.Services.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Services.Api;

public class AdminApiHandler : IAdminApiHandler
{
    private readonly string AdminsEdnpoint, AdminPasswordEndpoint;
    private readonly IHttpService HttpService;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AdminApiHandler(IConfiguration configuration, IHttpService httpService)
    {
        AdminsEdnpoint = configuration.GetSection("AppSettings:Endpoints:Admins").Value;
        AdminPasswordEndpoint = configuration.GetSection("AppSettings:Endpoints:AdminPassword").Value;
        HttpService = httpService;
    }

    public async Task<ResponseBag<CreateAdminResponse>> CreateNewAdmin(CreateAdminRequest request)
    {
        var httpClient = HttpService.GetHttpClient();

        var serialized = JsonSerializer.Serialize(request, JsonOptions);
        var requestContent = new StringContent(serialized, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(AdminsEdnpoint, requestContent);

        CreateAdminResponse? body = null;
        if (response!.StatusCode == HttpStatusCode.OK)
        {
            var stringBody = await response.Content.ReadAsStringAsync();
            body = JsonSerializer.Deserialize<CreateAdminResponse>(stringBody, JsonOptions);
        }

        return new ResponseBag<CreateAdminResponse>
        {
            Body = body,
            Code = response.StatusCode
        };
    }

    public async Task<ResponseBag<ResetAdminPasswordResponse>> ResetAdminPassword(ResetAdminPasswordRequest request)
    {
        var httpClient = HttpService.GetHttpClient();

        var serialized = JsonSerializer.Serialize(request, JsonOptions);
        var requestContent = new StringContent(serialized, Encoding.UTF8, "application/json");

        var response = await httpClient.PatchAsync(AdminPasswordEndpoint, requestContent);

        ResetAdminPasswordResponse? body = null;
        if (response!.StatusCode == HttpStatusCode.OK)
        {
            var stringBody = await response.Content.ReadAsStringAsync();
            body = JsonSerializer.Deserialize<ResetAdminPasswordResponse>(stringBody, JsonOptions);
        }

        return new ResponseBag<ResetAdminPasswordResponse>
        {
            Body = body,
            Code = response.StatusCode
        };
    }
}