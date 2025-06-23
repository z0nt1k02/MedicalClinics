using System.ComponentModel.DataAnnotations.Schema;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Core.Database.Entities;

public class CabinetEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FreeRecordEntity> FreeRecords { get; set; } = [];
    
    [ForeignKey("ClinicId")]
    public ClinicEntity? Clinic { get; set; }
    public Guid ClinicId { get; set; }
    
    public List<RecordOnClinicEntity> RecordsOnClinic { get; set; } = [];

}