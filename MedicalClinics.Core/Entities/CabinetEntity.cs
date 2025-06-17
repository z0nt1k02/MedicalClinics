using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalClinics.Core.Database.Entities;

public class CabinetEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FreeRecord> FreeRecords { get; set; } = [];
    
    [ForeignKey("ClinicId")]
    public ClinicEntity? Clinic { get; set; }
    public int ClinicId { get; set; }

}