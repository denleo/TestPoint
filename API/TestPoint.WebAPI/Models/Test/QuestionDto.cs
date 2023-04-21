using System.ComponentModel.DataAnnotations;
using TestPoint.Domain;

namespace TestPoint.WebAPI.Models.Test;

public class QuestionDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [Range(0, 2, ErrorMessage = "Incorrect questionType value, possible values: 0 - single option, 1 - multiple options, 2 - text substitution")]
    public QuestionType QuestionType { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MaxLength(1000, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? QuestionText { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "Question must contain at least one answer")]
    public AnswerDto[]? Answers { get; set; }
}
