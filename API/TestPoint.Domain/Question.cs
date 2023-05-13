namespace TestPoint.Domain;

public class Question : AuditableEntity
{
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }

    public ICollection<Answer>? Answers { get; set; }
}
