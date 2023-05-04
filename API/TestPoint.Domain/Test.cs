namespace TestPoint.Domain;

public class Test : AuditableEntity
{
    public string Name { get; set; }
    public TestDifficulty Difficulty { get; set; }
    public int EstimatedTime { get; set; }

    public Guid AuthorId { get; set; }
    public ICollection<Question> Questions { get; set; }
}
