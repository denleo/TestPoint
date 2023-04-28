using System.ComponentModel.DataAnnotations;
using TestPoint.Domain;

namespace TestPoint.WebAPI.Models.Test;

public class TestDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(256, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [Range(0, 2, ErrorMessage = "Incorrect difficulty value, possible values: 0 - easy, 1 - intermediate, 2 - advanced")]
    public TestDifficulty Difficulty { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [Range(1, 120, ErrorMessage = "Estimated time should be between {1} and {2} minutes")]
    public int EstimatedTime { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "Test must contain at least one question")]
    public QuestionDto[] Questions { get; set; }
}
