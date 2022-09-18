namespace TestPoint.Domain;

public class Administrator : Entity<int>
{
    public bool IsPasswordReset { get; set; }

    public int LoginId { get; set; }
    public SystemLogin Login { get; set; }
}