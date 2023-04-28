namespace TestPoint.Domain;

public class SystemLogin : AuditableEntity
{
    public LoginType LoginType { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool PasswordReseted { get; set; }
    public DateTime RegistryDate { get; set; }
}