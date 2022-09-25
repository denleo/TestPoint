using MediatR;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

public class CheckUserLoginQuery : IRequest<CheckUserLoginResponse?>
{
    public string Login { get; set; }
    public string Password { get; set; }
}