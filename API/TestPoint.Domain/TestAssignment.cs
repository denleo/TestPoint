namespace TestPoint.Domain;

public class TestAssignment : AuditableEntity
{
    public Guid TestId { get; set; }
    public Guid UserId { get; set; }

    public Test Test { get; set; }
    public User User { get; set; }

    public TestCompletion? TestCompletion { get; set; }
}
