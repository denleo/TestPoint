namespace TestPoint.Domain;

public class User : Entity<int>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] Avatar { get; set; }

    public int LoginId { get; set; }
    public SystemLogin Login { get; set; }
}