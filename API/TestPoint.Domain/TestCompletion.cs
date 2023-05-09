namespace TestPoint.Domain;

public class TestCompletion : AuditableEntity
{
    public Guid TestAssignmentId { get; set; }
    public double Score { get; set; }
    public double CompletionTime { get; set; }

    public ICollection<AnswerHistory> Answers { get; set; }
}
