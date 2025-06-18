using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface IFreeRecordService
{
    Task<FreeRecord> AddFreeRecord(CreateFreeRecordDto dto,Guid cabinetId);
    
}