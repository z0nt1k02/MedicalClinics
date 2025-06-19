using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface IFreeRecordService
{
    Task<FreeRecordEntity> AddFreeRecord(CreateFreeRecordDto dto,Guid cabinetId);
    bool DateCheck(DateTime targetDate, List<FreeRecordEntity> freeRecords);
    DateTime ParseDate(string date);

}