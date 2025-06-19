using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class CabinetService : ICabinetService
{
    private readonly IMedicalClinicsDbContext _context;
    private readonly IClinicService _clinicService;

    public CabinetService(IMedicalClinicsDbContext context,IClinicService clinicService)
    {
        _context = context;
        _clinicService = clinicService;
    }

    public async Task<CabinetEntity> GetCabinetById(Guid id)
    {
        CabinetEntity? cabinet = await _context.Cabinets
            .Include(f => f.FreeRecords)
            .Include(c=>c.Clinic)
            .FirstOrDefaultAsync(c => c.Id == id);
        if(cabinet == null)
            throw new NullReferenceException($"Cabinet with id {id} not found");
        return cabinet;
    }
    public async Task<CabinetEntity> GetCabinetWithFreeDaysByIdAsync(Guid id,int? month,int? year)
    {
        CabinetEntity cabinet = await GetCabinetById(id);
        
        var freeDates = cabinet.FreeRecords.AsQueryable();

        if (month.HasValue)
        {
            freeDates = freeDates.Where(fr=>fr.RecordDate.Month == month);
        }

        if (year.HasValue)
        {
            freeDates = freeDates.Where(fr => fr.RecordDate.Year == year);
        }

        if (!month.HasValue && !year.HasValue)
        {
            freeDates = freeDates.Where(fr => fr.RecordDate.Month == DateTime.UtcNow.Month);
        }

        var sortedDays =  freeDates
            .GroupBy(fr => fr.RecordDate.Date)
            .Select(g => g.First())
            .OrderBy(fr => fr.RecordDate).ToList();
            
        
        cabinet.FreeRecords = sortedDays;
        return cabinet;
        
    }
    
    public async Task<CabinetEntity> GetCabinetWithFreeHoursByIdAsync(Guid id,int day,int month,int year)
    {
        
        CabinetEntity cabinet = await GetCabinetById(id);
        var freeDates = cabinet.FreeRecords.AsQueryable();
        var sortedDates = freeDates
            .Where(fr => fr.RecordDate.Month == month && fr.RecordDate.Year == year && fr.RecordDate.Day == day)
            .OrderBy(fr => fr.RecordDate.TimeOfDay).ToList();
        
        
        cabinet.FreeRecords = sortedDates;
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
            FreeRecords = new List<FreeRecordEntity>(),
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