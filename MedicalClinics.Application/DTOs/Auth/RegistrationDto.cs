using System.ComponentModel.DataAnnotations;

namespace MedicalClinics.Application.DTOs.Auth;

public record RegistrationDto
(
    
    [Required(ErrorMessage = "Login is required")]
    [MinLength(4, ErrorMessage = "Login must be at least 4 characters long")]
    string login,
    
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
    [RegularExpression(@"^(?=.*[!@#$%^&*]).+$", 
        ErrorMessage = "Password must contain a special character (!@#$%^&*)")]
    string password
);