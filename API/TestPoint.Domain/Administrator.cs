namespace TestPoint.Domain;

public class Administrator : AuditableEntity
{
    public SystemLogin Login { get; set; }
}