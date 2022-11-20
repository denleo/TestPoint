using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Application.Users.Commands.SendForgotPasswordEmail
{
    public class SendForgotPasswordEmailHandler : IRequestHandler<SendForgotPasswordEmailCommand>
    {
        private readonly IUserDbContext _userDbContext;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public SendForgotPasswordEmailHandler(IUserDbContext userDbContext, IEmailService emailService, IJwtService jwtService)
        {
            _userDbContext = userDbContext;
            _emailService = emailService;
            _jwtService = jwtService;
        }

        public async Task<Unit> Handle(SendForgotPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userDbContext.Users
                .Where(x => x.Login.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with {request.Username} username does not exist");
            }

            if (!user.EmailConfirmed)
            {
                throw new EmailConfirmationException("User email is not confirmed");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id.ToString()),
                new("TokenType", "PasswordReset")
            };
            var token = _jwtService.CreateToken(claims, true);

            var message = new EmailMessage
            {
                Reciever = user.Email,
                Title = "Password Reset",
                Body = $"<h3><b>Hello, {user.FirstName}</b></h3>" + // TODO: REPLACE WITH HTML FILE
                       $"<div>The password reset request for your account was sent from the platform.</div>" +
                       $"<div>Click here to receive email with new password: <a href='{request.PasswordResetUrl + token}'>reset password</a></div>" +
                       "<div><b>Test Point System</b>, do not reply on this message.</div>"
            };

            _emailService.SendEmail(message);

            return Unit.Value;
        }
    }
}
