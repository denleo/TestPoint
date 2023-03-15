namespace TestPoint.Application.Admins.Queries.GetCurrentAdmin;

public class GetCurrentAdminResponse
{
    public Guid AdminId { get; set; }

    public string Username { get; set; }
    public bool PasswordReseted { get; set; }
    public DateTime RegistryDate { get; set; }
}