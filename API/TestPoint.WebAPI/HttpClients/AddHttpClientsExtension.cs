using TestPoint.WebAPI.HttpClients.Google;

namespace TestPoint.WebAPI.HttpClients;

public static class AddHttpClientsExtension
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration appConfig)
    {
        AddGoogleApiClient(services, appConfig);
    }

    private static void AddGoogleApiClient(IServiceCollection services, IConfiguration appConfig)
    {
        services.Configure<GoogleApiSettings>(appConfig.GetSection(nameof(GoogleApiSettings)));
        services.AddHttpClient<GoogleApiService>();
    }
}
