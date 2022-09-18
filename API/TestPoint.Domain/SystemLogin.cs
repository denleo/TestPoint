namespace TestPoint.Domain;

public class SystemLogin: Entity<int>
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegistryDate { get; set; }
}