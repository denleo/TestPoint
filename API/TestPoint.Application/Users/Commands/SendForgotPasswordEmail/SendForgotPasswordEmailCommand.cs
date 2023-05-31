using MediatR;

namespace TestPoint.Application.Users.Commands.SendForgotPasswordEmail;

public class SendForgotPasswordEmailCommand : IRequest
{
    public string Username { get; set; }
}
