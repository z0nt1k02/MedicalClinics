namespace MedicalClinics.Application.DTOs.Clinic;

public record ClinicDto(Guid id, string name, List<CabinetShortDto> cabinets);