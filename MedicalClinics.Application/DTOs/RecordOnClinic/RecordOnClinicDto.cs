using MedicalClinics.Application.DTOs.Clinic;

namespace MedicalClinics.Application.DTOs.RecordOnClinic;

public record RecordOnClinicDto(Guid id,
    Guid userId, 
    string date, 
    ClinicShortDto ClinicShortDto,
    CabinetShortDto CabinetShortDto,
    string contact, 
    string comment);