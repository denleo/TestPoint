using TestPoint.WebAPI.Models.Admin;

namespace TestPoint.WebAPI.Models.User;

public class UserDto : AdminDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}