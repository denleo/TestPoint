using MediatR;

namespace TestPoint.Application.Users.Commands.UnbindGoogleAccount;

public class UnbindGoogleAccountCommand : IRequest
{
    public Guid UserId { get; set; }
}
