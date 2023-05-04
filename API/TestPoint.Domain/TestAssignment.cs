namespace TestPoint.Domain;

public class TestAssignment : AuditableEntity
{
    public Guid TestId { get; set; }
    public Guid UserId { get; set; }

    public TestCompletion? TestCompletion { get; set; }
}
