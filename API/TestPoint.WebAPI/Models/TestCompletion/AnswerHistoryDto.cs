using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.TestCompletion;

public class AnswerHistoryDto
{
    [Required(ErrorMessage = "{0} field is required")]
    public Guid QuestionId { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "Question must contain at least one answer")]
    public string[] Answers { get; set; }
}
