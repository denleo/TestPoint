using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.EmailService;

public static class DependencyInjector
{
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, MailKitService>();
        return services;
    }
}