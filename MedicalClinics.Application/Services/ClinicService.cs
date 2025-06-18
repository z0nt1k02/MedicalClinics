using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using MedicalClinics.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class ClinicService : IClinicService
{
    private readonly MedicalClinicsDBContext _context;

    public ClinicService(MedicalClinicsDBContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<ClinicEntity>> GetAllClinicsAsync()
    {
        List<ClinicEntity> clinics = await _context.Clinics
            .Include(c=>c.Cabinets)
            .AsNoTracking()
            .ToListAsync();
        return clinics;
    }
    
    public async Task<ClinicEntity> GetClinicByIdAsync(int id)
    {
        var clinic = await _context.Clinics
            .Include(c=>c.Cabinets)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (clinic == null)
        {
            throw new NullReferenceException($"Clinic with id {id} was not found.");
        }
        return clinic;
    }
    
    public async Task<ClinicEntity> CreateClinicAsync(CreateClinicDto dto)
    {
        ClinicEntity newClinic = new ClinicEntity
        {
            Name = dto.name,
            Cabinets = new List<CabinetEntity>()
        };
        await _context.Clinics.AddAsync(newClinic);
        await _context.SaveChangesAsync();
        return newClinic;
    }

    public async Task DeleteClinicAsync(int id)
    {
        ClinicEntity? clinic = await _context.Clinics.FindAsync(id);
        if(clinic == null)
            throw new NullReferenceException($"Clinic with id {id} was not found.");
        _context.Clinics.Remove(clinic);
        await _context.SaveChangesAsync();
    }
}