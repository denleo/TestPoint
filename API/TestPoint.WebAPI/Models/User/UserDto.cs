using System.ComponentModel.DataAnnotations;
using TestPoint.WebAPI.Models.Admin;

namespace TestPoint.WebAPI.Models.User;

public class UserDto : AdminDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(254, ErrorMessage = "{0} field can be maximum {1} characters long")]
    [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Invalid {0} field format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(10, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? Password { get; set; }
}