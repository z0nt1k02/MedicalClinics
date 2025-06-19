using MedicalClinics.Application.DTOs.RecordOnClinic;
using MedicalClinics.Core.Database.Entities;

namespace MedicalClinics.Application.Mapping;

public static class RecordOnClinicMapper
{
    public static RecordOnClinicDto ToDto(this RecordOnClinicEntity entity)
    {
        return new RecordOnClinicDto(entity.Id,
            entity.UserId,entity.RecordDateOnUTC.ToString("yyyy-MM-dd HH:mm:ss"),
            entity.Cabinet!.Clinic!.ToShortDto(),
            entity.Cabinet!.ToShortDto(),
            entity.Contact,
            entity.Comment
            );
    }
}