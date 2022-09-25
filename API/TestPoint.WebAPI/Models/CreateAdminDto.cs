using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models;

public class CreateAdminDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(16, ErrorMessage = "{0} field can be maximum {1} characters long")]
    [RegularExpression("^[a-z|A-Z|\\d]+$", ErrorMessage = "Invalid {0} field format (a-z A-Z or numeric)")]
    public string Username { get; set; }
}