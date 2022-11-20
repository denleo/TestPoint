using TestPoint.Application.Common.Entities;

namespace TestPoint.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmail(EmailMessage message);
}