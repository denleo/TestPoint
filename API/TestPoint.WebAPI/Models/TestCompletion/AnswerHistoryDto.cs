using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.TestCompletion;

public class AnswerHistoryDto
{
    [Required(ErrorMessage = "{0} field is required")]
    public Guid QuestionId { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MaxLength(1000, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? AnswerText { get; set; }
}
