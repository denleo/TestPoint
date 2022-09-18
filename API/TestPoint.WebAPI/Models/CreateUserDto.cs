using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models;

public class CreateUserDto : CreateAdminDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(254, ErrorMessage = "{0} field can be maximum {1} characters long")]
    [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Invalid {0} field format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(1, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string LastName { get; set; }
}