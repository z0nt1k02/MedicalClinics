using MedicalClinics.Application.DTOs.RecordOnClinic;
using MedicalClinics.Core.Database.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface IRecordOnClinicService
{
    Task<List<RecordOnClinicEntity>> GetRecordsAdmin(Guid? cabinetId,int? clinicId);
    Task<RecordOnClinicEntity> AddRecordOnClinic(CreateRecordOnClinicDto dto,Guid userId);
    Task<List<RecordOnClinicEntity>> GetRecords(Guid userId);
}