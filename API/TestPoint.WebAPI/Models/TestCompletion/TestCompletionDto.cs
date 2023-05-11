namespace TestPoint.WebAPI.Models.TestCompletion;

public class TestCompletionDto
{
    public double Score { get; set; }
    public int CorrectAnswersCount { get; set; }
    public double CompletionTime { get; set; }
    public AnswerHistoryDto[] History { get; set; }
}
