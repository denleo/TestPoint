using MediatR;

namespace TestPoint.Application.Users.Commands.BindGoogleAccount;

public class BindGoogleAccountCommand : IRequest
{
    public Guid UserId { get; set; }
    public string GoogleSub { get; set; }
}
