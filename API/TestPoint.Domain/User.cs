namespace TestPoint.Domain;

public class User : AuditableEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public byte[]? Avatar { get; set; }

    public int LoginId { get; set; }
    public SystemLogin Login { get; set; }
}