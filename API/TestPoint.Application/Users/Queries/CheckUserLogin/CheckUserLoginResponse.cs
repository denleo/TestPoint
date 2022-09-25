using TestPoint.Domain;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

public class CheckUserLoginResponse
{
    public string Username { get; set; }
    public string Email { get; set; }
    public LoginType Role { get; set; }
}