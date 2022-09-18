using TestPoint.Application.Common.Enums;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginResponse
{
    public string Username { get; set; }
    public AccessRole Role { get; set; }
}