namespace TestPoint.WebAPI.Models.TestCompletion;

public class TestSubmitDto
{
    public Guid TestId { get; set; }
    public double CompletionTime { get; set; }
    public AnswerHistoryDto[] History { get; set; }
}
