using MedicalClinics.Application.DTOs;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface ICabinetService
{
    
    Task<CabinetEntity> GetCabinetByIdAsync(Guid id);
    Task<CabinetEntity> CreateCabinetAsync(CreateCabinetDto dto);
    Task DeleteCabinetAsync(Guid id);
}