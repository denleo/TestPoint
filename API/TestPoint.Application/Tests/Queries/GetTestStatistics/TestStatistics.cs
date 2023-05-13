namespace TestPoint.Application.Tests.Queries.GetTestStatistics;

public class TestStatistics
{
    public Guid TestId { get; set; }
    public double AverageScore { get; set; }
    public double AverageCompletionTime { get; set; }
    public QuestionStatistics[] QuestionStatistics { get; set; }
}

public class QuestionStatistics
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public int CorrectAnswersCount { get; set; }
    public int WrongAnswersCount { get; set; }
}
