using MediatR;
using Microsoft.Extensions.Configuration;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Application.Users.Commands.ResetUserPassword;

public class ResetUserPasswordHandler : IRequestHandler<ResetUserPasswordCommand, ResetUserPasswordResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;

    public ResetUserPasswordHandler(IUnitOfWork unitOfWork, IEmailService emailService, IConfiguration config)
    {
        _uow = unitOfWork;
        _emailService = emailService;
        _config = config;
    }

    public async Task<ResetUserPasswordResponse> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .FindOneAsync(x => x.Id == request.UserId);

        if (user == null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id does not exist.");
        }

        var timeout = int.Parse(_config.GetSection("DomainSettings:PasswordResetTimeout").Value);
        if (user.Login.PasswordReseted && timeout > 0 && DateTime.Now <= user.Login.UpdatedAt!.Value.AddHours(timeout))
        {
            throw new ActionNotAllowedException($"The password was recently reset, please wait {timeout} " +
                                       (timeout == 1 ? "hour" : "hours") + " before the next attempt.");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();
        user.Login.PasswordHash = PasswordHelper.ComputeHash(tempPassword);
        user.Login.PasswordReseted = true;

        await _uow.SaveChangesAsync(cancellationToken);

        var message = new EmailMessage
        {
            Reciever = user.Email,
            Title = "Password Reset",
            Body = EmailConstants.GetResetedPasswordResponse(user.FirstName, tempPassword)
        };

        _emailService.SendEmail(message);

        return new ResetUserPasswordResponse
        {
            TempPassword = tempPassword
        };
    }
}