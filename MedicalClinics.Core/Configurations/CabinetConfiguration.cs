using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class CabinetConfiguration : IEntityTypeConfiguration<CabinetEntity>
{
    public void Configure(EntityTypeBuilder<CabinetEntity> builder)
    {
        builder.HasMany(f => f.FreeRecords)
            .WithOne(c => c.Cabinet);
        
    }
}