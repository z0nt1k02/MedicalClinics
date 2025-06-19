using System.Globalization;
using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class FreeRecordService : IFreeRecordService
{
    private readonly IMedicalClinicsDbContext _context;
    private readonly ICabinetService _cabinetService;
    public FreeRecordService(IMedicalClinicsDbContext context,ICabinetService cabinetService)
    {
        _context = context;
        _cabinetService = cabinetService;
    }
    public async Task<FreeRecordEntity> AddFreeRecord(CreateFreeRecordDto dto,Guid cabinetId)
    {
        CabinetEntity? cabinet = await _context.Cabinets
            .Include(f=>f.FreeRecords)
            .FirstOrDefaultAsync(f => f.Id == cabinetId);
        
        if(cabinet == null)
            throw new NullReferenceException($"Cabinet with id {cabinetId} not found");
        DateTime parsedDate = ParseDate(dto.dateTime);
        
        bool result = DateCheck(parsedDate, cabinet.FreeRecords);
        if (result)
        {
            throw new Exception("This date already exists in the list");
        }

        FreeRecordEntity newFreeRecordEntity = new FreeRecordEntity
        {
            Id = Guid.NewGuid(),
            RecordDate = parsedDate,
            CabinetId = cabinetId,
            Cabinet = cabinet,
        };

        _context.Set<FreeRecordEntity>().Add(newFreeRecordEntity);
        await _context.SaveChangesAsync();
        return newFreeRecordEntity;
    }

    public bool DateCheck(DateTime targetDate, List<FreeRecordEntity> freeRecords)
    {
        var dateTimeSet = new HashSet<DateTime>(
            freeRecords
                .Where(r => r != null)
                .Select(r => r.RecordDate));
        
        bool result = dateTimeSet.Contains(targetDate);
        return result;
    }
    
    public DateTime ParseDate(string date)
    {
        DateTime newDate;
        try
        {
            DateTime parsedDateTime = DateTime.ParseExact
            (
                date,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture
            );
            newDate = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);
        }
        catch(FormatException ex)
        {
            throw new FormatException("Invalid date format",ex);
        }
        return newDate;
    }
    
    
}