using MediatR;

namespace TestPoint.Application.Admins.Commands.ChangePassword;

public class ChangeAdminPasswordCommand : IRequest
{
    public Guid AdminId { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}
