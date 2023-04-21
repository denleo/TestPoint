using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.UserGroup;

public class UserGroupDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(256, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? GroupName { get; set; }
}
