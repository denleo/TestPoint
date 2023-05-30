namespace TestPoint.WebAPI.Models.TestCompletion;

public class AnswerHistoryDto
{
    public Guid QuestionId { get; set; }
    public string[] Answers { get; set; }
}
