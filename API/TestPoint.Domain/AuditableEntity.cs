namespace TestPoint.Domain;

public abstract class AuditableEntity : Entity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}