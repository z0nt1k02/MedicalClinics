using MedicalClinics.Application.DTOs;
using MedicalClinics.Core.Database.Entities;

namespace MedicalClinics.Application.Mapping;

public static class CabinetMapper
{
    public static CabinetShortDto ToShortDto(this CabinetEntity entity)
    {
        return new CabinetShortDto(entity.Id.ToString(), entity.Name);
    }
    
    public static CabinetDto ToDtoDays(this CabinetEntity entity)
    {
        List<string> freeDates = [];
        foreach (var freeRecord in entity.FreeRecords)
        {
            freeDates.Add(freeRecord.RecordDate.ToString("dd.MM.yyyy"));
        }
        return new CabinetDto(entity.Id.ToString(),entity.Name,entity.Clinic!.Name,entity.Id.ToString(),freeDates);
    }
    
    public static CabinetDto ToDtoHours(this CabinetEntity entity)
    {
        List<string> freeDates = [];
        foreach (var freeRecord in entity.FreeRecords)
        {
            freeDates.Add(freeRecord.RecordDate.ToString("HH:mm"));
        }
        return new CabinetDto(entity.Id.ToString(),entity.Name,entity.Clinic!.Name,entity.Id.ToString(),freeDates);
    }

    /*public static CabinetFreeRecordsHoursDto ToFreeHoursDto(this CabinetEntity entity, DateTime date)
    {
        List<string> freeDatesHours = [];
        foreach (var freeRecord in entity.FreeRecords)
        {
            if (freeRecord.RecordDate.Date == date.Date)
            {
                freeDatesHours.Add(freeRecord.RecordDate.ToString("HH:mm"));
            }
            
        }
        return new CabinetFreeRecordsHoursDto(entity.Id.ToString(),entity.Name,entity.Clinic!.Name,entity.Id.ToString(),freeDates);
    }*/
}