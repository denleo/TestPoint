using MediatR;

namespace TestPoint.Application.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest
{
    public int UserId { get; set; }
    public string EmailForConfirmation { get; set; }
}