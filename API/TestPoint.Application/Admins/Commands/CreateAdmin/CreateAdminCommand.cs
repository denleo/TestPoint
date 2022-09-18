using MediatR;

namespace TestPoint.Application.Admins.Commands.CreateAdmin;

public class CreateAdminCommand : IRequest<CreateAdminResponse>
{
    public string Username { get; set; }

    public string Password { get; set; }
}