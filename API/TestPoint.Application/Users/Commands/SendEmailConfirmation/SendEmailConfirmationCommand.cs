using MediatR;

namespace TestPoint.Application.Users.Commands.SendEmailConfirmation;

public class SendEmailConfirmationCommand : IRequest
{
    public Guid UserId { get; set; }
}