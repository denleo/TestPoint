using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.TestCompletion;

public class TestSubmitDto
{
    [Required(ErrorMessage = "{0} field is required")]
    public Guid TestId { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Completion time should be greater than zero")]
    public double CompletionTime { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "Test completion must contain at least one answer")]
    public AnswerHistoryDto[] History { get; set; }
}
