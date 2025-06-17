using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<ClinicEntity>
{
    public void Configure(EntityTypeBuilder<ClinicEntity> builder)
    {
        builder.HasMany(c => c.Cabinets)
            .WithOne(c => c.Clinic);
    }
}