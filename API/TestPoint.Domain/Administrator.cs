namespace TestPoint.Domain;

public class Administrator : AuditableEntity<int>
{
    public int LoginId { get; set; }
    public SystemLogin Login { get; set; }
}