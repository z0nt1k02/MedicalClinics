using MedicalClinics.Application.DTOs.FreeRecords;

namespace MedicalClinics.Application.DTOs;

public record CabinetDto(string id,string name,string clinicName,string clinicId,List<string> recordsDays);