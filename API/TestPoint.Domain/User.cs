namespace TestPoint.Domain;

public class User : AuditableEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public byte[]? Avatar { get; set; }

    public SystemLogin Login { get; set; }
}