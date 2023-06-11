namespace TestPoint.Domain;

public class UserGoogleAccountMapping : AuditableEntity
{
    public Guid UserId { get; set; }
    public string? GoogleSub { get; set; }
}
