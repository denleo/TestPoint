namespace TestPoint.Domain;

public class Answer : AuditableEntity
{
    public string? AnswerText { get; set; }
    public bool IsCorrect { get; set; }
}
