using TestPoint.Domain;

namespace TestPoint.WebAPI.Models.Test;

public class QuestionDto
{
    public QuestionType QuestionType { get; set; }
    public string QuestionText { get; set; }
    public AnswerDto[] Answers { get; set; }
}
