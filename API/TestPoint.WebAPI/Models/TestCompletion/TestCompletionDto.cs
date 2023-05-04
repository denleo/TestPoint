using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.TestCompletion;

public class TestCompletionDto
{
    [Required(ErrorMessage = "{0} field is required")]
    public Guid TestId { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Score should be equal or greater than zero")]
    public double Score { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Completion time should be greater than zero")]
    public double CompletionTime { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "Test completion must contain at least one answer")]
    public AnswerHistoryDto[]? Answers { get; set; }
}
