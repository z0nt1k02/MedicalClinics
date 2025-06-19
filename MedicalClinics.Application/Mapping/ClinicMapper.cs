using MedicalClinics.Application.DTOs.Clinic;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Mapping;

public static class ClinicMapper
{
    public static ClinicDto ToDto(this ClinicEntity clinicEntity)
    {
        return new ClinicDto
        (
            clinicEntity.Id,
            clinicEntity.Name,
            clinicEntity.Cabinets.Select(c=>c.ToShortDto()).ToList()
        );
    }

    public static ClinicShortDto ToShortDto(this ClinicEntity clinicEntity)
    {
        return new ClinicShortDto(clinicEntity.Id,clinicEntity.Name);
    }
}