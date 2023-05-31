using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.EmailService;

public static class DependencyInjector
{
    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration appConfig)
    {
        var emailSettings = appConfig.GetSection(nameof(EmailServiceSettings)).Get<EmailServiceSettings>();

        if (emailSettings is null)
        {
            return services;
        }

        services.Configure<EmailServiceSettings>(appConfig.GetSection(nameof(EmailServiceSettings)));
        services.AddSingleton<IEmailService, MailKitService>();

        return services;
    }
}