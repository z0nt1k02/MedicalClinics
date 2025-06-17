namespace MedicalClinics.Core.Database.Entities;

public class ClinicEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CabinetEntity> Cabinets { get; set; } = [];
}