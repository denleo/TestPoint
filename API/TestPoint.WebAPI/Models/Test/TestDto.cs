using TestPoint.Domain;

namespace TestPoint.WebAPI.Models.Test;

public class TestDto
{
    public string Name { get; set; }
    public TestDifficulty Difficulty { get; set; }
    public int EstimatedTime { get; set; }
    public QuestionDto[] Questions { get; set; }
}
