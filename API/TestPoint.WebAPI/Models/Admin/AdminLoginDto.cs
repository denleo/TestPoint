﻿using System.ComponentModel.DataAnnotations;

namespace TestPoint.WebAPI.Models.Admin;

public class AdminLoginDto
{
    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(5, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(16, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "{0} field is required")]
    [MinLength(10, ErrorMessage = "{0} field must be at least {1} characters long")]
    [MaxLength(64, ErrorMessage = "{0} field can be maximum {1} characters long")]
    public string? Password { get; set; }
}