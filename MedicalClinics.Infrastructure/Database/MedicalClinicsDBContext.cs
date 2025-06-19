using MedicalClinics.Application.Interfaces;
using MedicalClinics.Core.Configurations;
using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalClinics.Infrastructure.Database;

public class MedicalClinicsDBContext : DbContext,IMedicalClinicsDbContext
{
    public DbSet<ClinicEntity> Clinics { get; set; }
    public DbSet<CabinetEntity> Cabinets { get; set; }
    public DbSet<RecordOnClinic> RecordOnClinics { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FreeRecord>();
        modelBuilder.ApplyConfiguration(new CabinetConfiguration());
        modelBuilder.ApplyConfiguration(new ClinicConfiguration());
        modelBuilder.ApplyConfiguration(new FreeRecordConfiguration());
    }
    
    public MedicalClinicsDBContext(DbContextOptions<MedicalClinicsDBContext> options) : base(options)
    {
        
    }
}