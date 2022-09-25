using TestPoint.Domain;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginResponse
{
    public string Username { get; set; }
    public LoginType Role { get; set; }
}