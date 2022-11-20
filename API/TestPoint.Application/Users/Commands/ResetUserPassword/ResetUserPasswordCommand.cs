using MediatR;

namespace TestPoint.Application.Users.Commands.ResetUserPassword;

public class ResetUserPasswordCommand : IRequest<ResetUserPasswordResponse>
{
    public int UserId { get; set; }
}