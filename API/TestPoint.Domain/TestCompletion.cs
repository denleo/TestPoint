namespace TestPoint.Domain;

public class TestCompletion : AuditableEntity
{
    public double Score { get; set; }
    public double CompletionTime { get; set; }

    public ICollection<AnswerHistory>? Answers { get; set; }
}
