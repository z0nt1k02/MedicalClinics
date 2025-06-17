using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalClinics.Core.Database.Entities;

public class FreeRecord
{
    public Guid Id { get; set; }
    public DateTime RecordDate { get; set; }
    
    [ForeignKey("CabinetId")]
    public CabinetEntity? Cabinet { get; set; }
    
    public Guid CabinetId { get; set; }
}