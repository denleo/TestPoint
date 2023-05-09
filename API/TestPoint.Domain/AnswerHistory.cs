namespace TestPoint.Domain;

public class AnswerHistory : AuditableEntity
{
    public Guid QuestionId { get; set; }
    public string AnswerText { get; set; }
}
