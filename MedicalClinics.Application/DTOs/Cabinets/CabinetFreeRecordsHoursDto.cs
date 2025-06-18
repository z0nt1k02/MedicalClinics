namespace MedicalClinics.Application.DTOs;

public record CabinetFreeRecordsHoursDto(string id,string name,string clinicName,string clinicId,List<string> recordsHours);