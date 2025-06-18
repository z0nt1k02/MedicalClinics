using MedicalClinics.Application.DTOs;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces;

public interface IClinicService
{
    Task<IReadOnlyList<ClinicEntity>> GetAllClinicsAsync();
    Task<ClinicEntity> GetClinicByIdAsync(int id);
    Task<ClinicEntity> CreateClinicAsync(CreateClinicDto dto);
    Task DeleteClinicAsync(int id);
}