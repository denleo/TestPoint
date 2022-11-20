using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Application.Users.Commands.ResetUserPassword;

public class ResetUserPasswordHandler : IRequestHandler<ResetUserPasswordCommand, ResetUserPasswordResponse>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;

    public ResetUserPasswordHandler(IUserDbContext userDbContext, IEmailService emailService, IConfiguration config)
    {
        _userDbContext = userDbContext;
        _emailService = emailService;
        _config = config;
    }

    public async Task<ResetUserPasswordResponse> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users
            .Include(x => x.Login)
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new EntityNotFoundException("User does not exist.");
        }

        var timeout = int.Parse(_config.GetSection("DomainSettings:PasswordResetTimeout").Value);
        if (user.Login.PasswordReseted && timeout > 0 && DateTime.Now <= user.Login.UpdatedAt.AddHours(timeout))
        {
            throw new TimeoutException($"The password was recently reset, please wait {timeout} " +
                                       (timeout == 1 ? "hour" : "hours") + " before the next attempt.");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();
        user.Login.PasswordHash = PasswordHelper.ComputeHash(tempPassword);
        user.Login.PasswordReseted = true;

        await _userDbContext.SaveChangesAsync(cancellationToken);

        var message = new EmailMessage
        {
            Reciever = user.Email,
            Title = "Password Reset",
            Body = $"<h3><b>Hello, {user.FirstName}</b></h3>" + // TODO: REPLACE WITH HTML FILE
                   $"<div>Here is your new temporary password to enter the platform: <b>{tempPassword}</b></div>" +
                   "<div><b>Test Point System</b>, do not reply on this message.</div>"
        };

        _emailService.SendEmail(message);

        return new ResetUserPasswordResponse
        {
            TempPassword = tempPassword
        };
    }
}