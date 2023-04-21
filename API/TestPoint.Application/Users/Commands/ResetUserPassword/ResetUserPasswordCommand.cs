using MediatR;

namespace TestPoint.Application.Users.Commands.ResetUserPassword;

public class ResetUserPasswordCommand : IRequest<ResetUserPasswordResponse>
{
    public Guid UserId { get; set; }
}