using System.ComponentModel.DataAnnotations;

namespace MedicalClinics.Application.DTOs.Auth;

public record LoginDto
(
    [Required(ErrorMessage = "Login is required")]
    string login, 
    
    [Required(ErrorMessage = "Password is required")]
    string password);