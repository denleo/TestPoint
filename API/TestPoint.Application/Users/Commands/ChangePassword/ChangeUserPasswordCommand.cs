using MediatR;

namespace TestPoint.Application.Users.Commands.ChangePassword;

public class ChangeUserPasswordCommand : IRequest
{
    public Guid UserId { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}
