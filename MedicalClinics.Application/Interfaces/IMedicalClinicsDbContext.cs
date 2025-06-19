using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Application.Interfaces;

public interface IMedicalClinicsDbContext
{
    public DbSet<ClinicEntity> Clinics { get; set; }
    public DbSet<CabinetEntity> Cabinets { get; set; }
    public DbSet<RecordOnClinic> RecordOnClinics { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    
    DbSet<T> Set<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}