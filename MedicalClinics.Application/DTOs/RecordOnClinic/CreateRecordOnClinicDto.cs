using System.ComponentModel.DataAnnotations;

namespace MedicalClinics.Application.DTOs.RecordOnClinic;

public record CreateRecordOnClinicDto(
    [Required]
    string date,
    
    [Required]
    int clinicId,
    
    [Required]
    string contact,
    
    [Required]
    Guid cabinetId,
    
    string comment);