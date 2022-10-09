using Core.Models.Api;
using Core.Models.Api.CreateAdmin;
using Core.Models.Api.ResetAdminPassword;

namespace Core.Services.Api;

public interface IAdminApiHandler
{
    /// <exception cref="HttpRequestException">
    /// Thrown when failed to establish connection.
    /// </exception>
    Task<ResponseBag<CreateAdminResponse>> CreateNewAdmin(CreateAdminRequest request);

    /// <exception cref="HttpRequestException">
    /// Thrown when failed to establish connection.
    /// </exception>
    Task<ResponseBag<ResetAdminPasswordResponse>> ResetAdminPassword(ResetAdminPasswordRequest request);
}