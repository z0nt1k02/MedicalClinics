using System.ComponentModel.DataAnnotations;

namespace MedicalClinics.Application.DTOs.Auth;

public record AuthDto(
    [Required(ErrorMessage = "Login is required")]
    [MinLength(6, ErrorMessage = "Login must be at least 6 characters long")]
    string login,
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
    string password);