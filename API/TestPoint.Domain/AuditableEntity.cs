namespace TestPoint.Domain;

public abstract class AuditableEntity<T> : Entity<T>
{
    public DateTime UpdatedAt { get; set; }
}