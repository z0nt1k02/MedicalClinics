using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using MedicalClinics.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class CabinetService : ICabinetService
{
    private readonly MedicalClinicsDBContext _context;
    private readonly IClinicService _clinicService;

    public CabinetService(MedicalClinicsDBContext context,IClinicService clinicService)
    {
        _context = context;
        _clinicService = clinicService;
    }
    public async Task<CabinetEntity> GetCabinetByIdAsync(Guid id)
    {
        CabinetEntity? cabinet = await _context.Cabinets
            .Include(f => f.FreeRecords)
            .Include(c=>c.Clinic)
            .FirstOrDefaultAsync(c => c.Id == id);
        if(cabinet == null)
            throw new NullReferenceException($"Cabinet with id {id} not found");
        return cabinet;
    }

    public async Task<CabinetEntity> CreateCabinetAsync(CreateCabinetDto dto)
    {
        ClinicEntity clinic = await _clinicService.GetClinicByIdAsync(Int32.Parse(dto.clinicId));
        if (clinic == null)
        {
            throw new NullReferenceException($"Clinic with id {dto.clinicId} not found");
        }

        CabinetEntity newCabinet = new CabinetEntity
        {
            Id = Guid.NewGuid(),
            Name = dto.name,
            ClinicId = int.Parse(dto.clinicId),
            Clinic = clinic,
            FreeRecords = new List<FreeRecord>(),
        };
        await _context.Cabinets.AddAsync(newCabinet);
        
        await _context.SaveChangesAsync();
        return newCabinet;
    }

    public async Task DeleteCabinetAsync(Guid id)
    {
        CabinetEntity? cabinet = await _context.Cabinets.FindAsync(id);
        if(cabinet == null)
            throw new NullReferenceException($"Cabinet with id {id} not found");
        _context.Cabinets.Remove(cabinet);
        await _context.SaveChangesAsync();
    }
}