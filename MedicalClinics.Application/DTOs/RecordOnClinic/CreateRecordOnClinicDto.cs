using System.ComponentModel.DataAnnotations;

namespace MedicalClinics.Application.DTOs.RecordOnClinic;

/// <param name="date">Дата и время записи. Должны быть переданы в формате "dd.MM.yyyy HH:mm" (например: "12.01.2025 18:00")</param>
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