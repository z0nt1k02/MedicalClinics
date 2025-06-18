namespace MedicalClinics.Application.DTOs.Clinic;

public record ClinicDto(int id, string name, List<CabinetShortDto> cabinets);