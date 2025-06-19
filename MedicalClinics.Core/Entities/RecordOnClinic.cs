using System.ComponentModel.DataAnnotations.Schema;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Core.Database.Entities;

public class RecordOnClinic
{
    public Guid Id { get; set; }
    
    public int ClinicId { get; set; }
    public Guid CabinetId { get; set; }
    
    [ForeignKey(("ClinicId"))]
    public ClinicEntity? Clinic { get; set; }
    
    [ForeignKey(("CabinetId"))]
    public CabinetEntity? Cabinet { get; set; }
    
    public DateTime RecordDateOnUTC { get; set; }
    
    public string Comment { get; set; } = string.Empty;
    
    public Guid UserId { get; set; }
}