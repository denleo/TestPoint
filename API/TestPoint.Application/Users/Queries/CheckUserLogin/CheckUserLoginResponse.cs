using TestPoint.Application.Common.Enums;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

public class CheckUserLoginResponse
{
    public string Username { get; set; }
    public string Email { get; set; }
    public AccessRole Role { get; set; }
}