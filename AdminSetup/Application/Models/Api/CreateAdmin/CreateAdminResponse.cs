namespace Core.Models.Api.CreateAdmin;

public class CreateAdminResponse
{
    public Guid AdminId { get; set; }
    public string TempPassword { get; set; }
}