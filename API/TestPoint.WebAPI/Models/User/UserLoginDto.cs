using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.User;

public class UserLoginDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(254, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string Login { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(10, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string Password { get; set; }
}