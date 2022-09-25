using MediatR;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginQuery : IRequest<CheckAdminLoginResponse?>
{
    public string Username { get; set; }
    public string Password { get; set; }
}