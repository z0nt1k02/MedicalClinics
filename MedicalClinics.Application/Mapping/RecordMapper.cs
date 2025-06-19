using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Core.Database.Entities;

namespace MedicalClinics.Application.Mapping;

public static class RecordMapper
{
    public static FreeRecordDto ToDto(this FreeRecordEntity entity)
    {
        return new FreeRecordDto(entity.Id, entity.CabinetId, entity.RecordDate.ToString());
    }
}