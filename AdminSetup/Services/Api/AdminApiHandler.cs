using Core.Models.Api;
using Core.Models.Api.CreateAdmin;
using Core.Models.Api.ResetAdminPassword;
using Core.Services.Api;
using Microsoft.Extensions.Configuration;

namespace Services.Api;

public class AdminApiHandler : IAdminApiHandler
{
    private readonly IConfiguration _configuration;

    public AdminApiHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<ResponseBag<CreateAdminResponse>> CreateNewAdmin(CreateAdminRequest request)
    {
        var apiKey = _configuration.GetSection("AppSettings:Key").Value;
        var adminsEndpoint = _configuration.GetSection("AppSettings:Endpoints:Admins").Value;

        return new RequestHandler(apiKey)
            .SendRequest<CreateAdminRequest, CreateAdminResponse>(adminsEndpoint, RequestMethod.POST, request);

    }

    public Task<ResponseBag<ResetAdminPasswordResponse>> ResetAdminPassword(ResetAdminPasswordRequest request)
    {
        var apiKey = _configuration.GetSection("AppSettings:Key").Value;
        var adminPasswordEndpoint = _configuration.GetSection("AppSettings:Endpoints:AdminPassword").Value;

        return new RequestHandler(apiKey)
            .SendRequest<ResetAdminPasswordRequest, ResetAdminPasswordResponse>(adminPasswordEndpoint, RequestMethod.PATCH, request);
    }
}