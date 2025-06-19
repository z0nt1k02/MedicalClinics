using MedicalClinics.Application.DTOs.RecordOnClinic;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Services;

public class RecordOnClinicService : IRecordOnClinicService
{
    private readonly IMedicalClinicsDbContext _context;
    private readonly ICabinetService _cabinetService;
    private readonly IClinicService _clinicService;
    private readonly IFreeRecordService _freeRecordService;

    public RecordOnClinicService(IMedicalClinicsDbContext context,ICabinetService cabinetService,IClinicService clinicService,IFreeRecordService freeRecordService)
    {
     _context = context;   
     _cabinetService = cabinetService;
     _clinicService = clinicService;
     _freeRecordService = freeRecordService;
    }
    
    public async Task<List<RecordOnClinicEntity>> GetRecordsAdmin(Guid? cabinetId,int? clinicId)
    {
        var recordsOnClinic = _context.RecordOnClinics
            .AsNoTracking()
            .Include(c => c.Cabinet)
            .ThenInclude(c=>c!.Clinic).AsQueryable();

        if (cabinetId.HasValue)
        {
            CabinetEntity cabinet = await _cabinetService.GetCabinetById(cabinetId.Value);
            recordsOnClinic = recordsOnClinic.Where(c => c.CabinetId == cabinetId.Value);
            return recordsOnClinic.ToList();
        }
        if (clinicId.HasValue)
        {
            ClinicEntity clinic = await _clinicService.GetClinicByIdAsync(clinicId.Value);
            recordsOnClinic = recordsOnClinic.Where(c => c.Cabinet!.ClinicId == clinicId);
            return recordsOnClinic.ToList();
        }

        if (!clinicId.HasValue && !cabinetId.HasValue)
        {
            recordsOnClinic = recordsOnClinic
                .Where(c => c.RecordDateOnUTC.Month == DateTime.Now.Month && c.RecordDateOnUTC.Year == DateTime.Now.Year);
            return recordsOnClinic.ToList();
                
        }
        
        return recordsOnClinic.ToList();
    }

    public async Task<RecordOnClinicEntity> AddRecordOnClinic(CreateRecordOnClinicDto dto,Guid userId)
    {
        DateTime recordDate = _freeRecordService.ParseDate(dto.date);
        CabinetEntity cabinet = await _cabinetService.GetCabinetById(dto.cabinetId);
        bool checkDate = _freeRecordService.DateCheck(recordDate, cabinet.FreeRecords);
        if (!checkDate)
        {
            throw new ApplicationException("This date is not available for recording");
        }

        RecordOnClinicEntity newRecordOnClinicEntity = new RecordOnClinicEntity
        {
            Id = Guid.NewGuid(),
            CabinetId = cabinet.Id,
            ClinicId = cabinet.ClinicId,
            Cabinet = cabinet,
            Clinic = cabinet.Clinic,
            RecordDateOnUTC = recordDate,
            UserId = userId,
            Contact = dto.contact,
            Comment = dto.comment,
        };
        FreeRecordEntity deleteFreeRecord = cabinet.FreeRecords.First(fr=>fr.RecordDate==recordDate);
        cabinet.FreeRecords.Remove(deleteFreeRecord);
        _context.RecordOnClinics.Add(newRecordOnClinicEntity);
        await _context.SaveChangesAsync();
        return newRecordOnClinicEntity;
    }

    public async Task<List<RecordOnClinicEntity>> GetRecords(Guid userId)
    {
        List<RecordOnClinicEntity> records = await _context.RecordOnClinics
            .Include(c=>c.Cabinet)
            .ThenInclude(cl=>cl.Clinic)
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .ToListAsync();
        return records;
    }
}