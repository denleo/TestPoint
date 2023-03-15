namespace TestPoint.Application.Admins.Commands.CreateAdmin;

public class CreateAdminResponse
{
    public Guid AdminId { get; set; }
    public string TempPassword { get; set; }
}