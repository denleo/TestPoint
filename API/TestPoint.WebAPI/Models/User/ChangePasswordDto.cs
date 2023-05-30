namespace TestPoint.WebAPI.Models.User;

public class ChangePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
