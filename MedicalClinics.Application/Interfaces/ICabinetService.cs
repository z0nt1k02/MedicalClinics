using MedicalClinics.Application.DTOs;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface ICabinetService
{
    
    Task<CabinetEntity> GetCabinetWithFreeDaysByIdAsync(Guid id,int? month,int? year);
    Task<CabinetEntity> GetCabinetWithFreeHoursByIdAsync(Guid id,int day,int month,int year);
    Task<CabinetEntity> CreateCabinetAsync(CreateCabinetDto dto);
    Task DeleteCabinetAsync(Guid id);
}