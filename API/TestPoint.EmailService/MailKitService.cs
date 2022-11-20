using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.EmailService;

public class MailKitService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogService _logService;

    public MailKitService(IConfiguration configuration, ILogService logService)
    {
        _config = configuration;
        _logService = logService;
    }

    public async Task SendEmail(EmailMessage message)
    {
        GetConfigValues(out var smtpHost, out var smtpPort, out var username, out var password);

        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Test Point System", username));
        emailMessage.To.Add(MailboxAddress.Parse(message.Reciever));
        emailMessage.Subject = message.Title;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Body
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(smtpHost, smtpPort, true);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            _logService.Log<MailKitService>(LogLevel.Error, "Email Service Error", ex);
        }
    }

    private void GetConfigValues(out string smtpHost, out int smtpPort, out string username, out string password)
    {
        smtpHost = _config.GetSection("EmailService:Host").Value;
        smtpPort = int.Parse(_config.GetSection("EmailService:Port").Value);
        username = _config.GetSection("EmailService:Username").Value;
        password = _config.GetSection("EmailService:Password").Value;
    }
}