using System.Globalization;
using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class FreeFreeRecordService : IFreeRecordService
{
    private readonly MedicalClinicsDBContext _context;
    

    public FreeFreeRecordService(MedicalClinicsDBContext context)
    {
        _context = context;
    }
    public async Task<FreeRecord> AddFreeRecord(CreateFreeRecordDto dto,Guid cabinetId)
    {
        CabinetEntity? cabinet = await _context.Cabinets
            .Include(f=>f.FreeRecords)
            .FirstOrDefaultAsync(f => f.Id == cabinetId);
        if(cabinet == null)
            throw new NullReferenceException($"Cabinet with id {cabinetId} not found");
        DateTime parsedDate = ParseDate(dto.dateTime);
        Console.WriteLine(parsedDate);
        bool result = DateCheck(parsedDate, cabinet.FreeRecords);
        if (result)
        {
            throw new Exception("This date already exists in the list");
        }

        FreeRecord newFreeRecord = new FreeRecord
        {
            Id = Guid.NewGuid(),
            RecordDate = parsedDate,
            CabinetId = cabinetId,
            Cabinet = cabinet,
        };

        _context.Set<FreeRecord>().Add(newFreeRecord);
        await _context.SaveChangesAsync();
        return newFreeRecord;
        


    }

    private bool DateCheck(DateTime targetDate, List<FreeRecord> freeRecords)
    {
        var dateTimeSet = new HashSet<DateTime>(
            freeRecords
                .Where(r => r != null)
                .Select(r => r.RecordDate));
        
        bool result = dateTimeSet.Contains(targetDate);
        return result;
    }

    private DateTime ParseDate(string date)
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
            newDate = TimeZoneInfo.ConvertTimeToUtc(parsedDateTime,TimeZoneInfo.Local);
        }
        catch(FormatException ex)
        {
            throw new FormatException("Invalid date format");
        }
        return newDate;
    }
}