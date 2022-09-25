using MediatR;

namespace TestPoint.Application.Admins.Commands.ResetAdminPassword;

/// <summary>
/// Admin Setup Tool command
/// </summary>
public class ResetAdminPasswordCommand : IRequest<ResetAdminPasswordResponse>
{
    public string Username { get; set; }
}