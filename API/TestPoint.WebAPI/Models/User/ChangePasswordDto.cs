using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.User;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(10, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(10, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string NewPassword { get; set; }
}
