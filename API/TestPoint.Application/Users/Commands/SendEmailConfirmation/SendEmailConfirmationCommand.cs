using MediatR;

namespace TestPoint.Application.Users.Commands.SendEmailConfirmation;

public class SendEmailConfirmationCommand : IRequest
{
    public int UserId { get; set; }
    public string EmailConfirmUrl { get; set; }
}