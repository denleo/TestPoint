using MediatR;

namespace TestPoint.Application.Admins.Commands.CreateAdmin;

/// <summary>
/// Admin Setup Tool command
/// </summary>
public class CreateAdminCommand : IRequest<CreateAdminResponse>
{
    public string Username { get; set; }
}