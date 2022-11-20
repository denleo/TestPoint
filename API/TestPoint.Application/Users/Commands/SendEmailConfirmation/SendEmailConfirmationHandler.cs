using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestPoint.Application.Common.Entities;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Services;

namespace TestPoint.Application.Users.Commands.SendEmailConfirmation
{
    public class SendEmailConfirmationHandler : IRequestHandler<SendEmailConfirmationCommand>
    {
        private readonly IUserDbContext _userDbContext;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public SendEmailConfirmationHandler(IUserDbContext userDbContext, IJwtService jwtService, IEmailService emailService)
        {
            _userDbContext = userDbContext;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userDbContext.Users
                .Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                throw new EntityNotFoundException($"User with {request.UserId} id does not exist");
            }

            if (user.EmailConfirmed)
            {
                throw new EmailConfirmationException("User email is already confirmed");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new("TokenType", "EmailConfirmation")
            };
            var token = _jwtService.CreateToken(claims, true);

            var message = new EmailMessage
            {
                Reciever = user.Email,
                Title = "Email Confirmation",
                Body = $"<h3><b>Hello, {user.FirstName}</b></h3>" + // TODO: REPLACE WITH HTML FILE
                       $"<div>Follow the link to complete email confirmation: <a href='{request.EmailConfirmUrl + token}'>confirm email</a></div>" +
                       "<div><b>Test Point System</b>, do not reply on this message.</div>"
            };

            _emailService.SendEmail(message);

            return Unit.Value;
        }
    }
}
