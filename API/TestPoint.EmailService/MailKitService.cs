using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.EmailService;

public class MailKitService : IEmailService
{
    private readonly EmailServiceSettings _emailOptions;
    private readonly ILogger<MailKitService> _logger;

    public MailKitService(IOptions<EmailServiceSettings> emailOptions, ILogger<MailKitService> logger)
    {
        _emailOptions = emailOptions.Value;
        _logger = logger;
    }

    public async Task SendEmail(EmailMessage message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Test Point System", _emailOptions.Username));
        emailMessage.To.Add(MailboxAddress.Parse(message.Reciever));
        emailMessage.Subject = message.Title;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Body
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, true);
            await client.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
        catch (Exception? ex)
        {
            _logger.LogError(ex, "Email Service Error");
        }
    }
}