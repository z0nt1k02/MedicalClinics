﻿using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class ClinicService : IClinicService
{
    private readonly IMedicalClinicsDbContext _context;

    public ClinicService(IMedicalClinicsDbContext context)
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
    
    public async Task<ClinicEntity> GetClinicByIdAsync(Guid id)
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
            Id = Guid.NewGuid(),
            Name = dto.name,
            Cabinets = new List<CabinetEntity>()
        };
        await _context.Clinics.AddAsync(newClinic);
        await _context.SaveChangesAsync();
        return newClinic;
    }

    public async Task DeleteClinicAsync(Guid id)
    {
        ClinicEntity? clinic = await _context.Clinics.FindAsync(id);
        if(clinic == null)
            throw new NullReferenceException($"Clinic with id {id} was not found.");
        _context.Clinics.Remove(clinic);
        await _context.SaveChangesAsync();
    }
}