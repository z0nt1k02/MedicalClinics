using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class FreeRecordConfiguration : IEntityTypeConfiguration<FreeRecordEntity>
{
    public void Configure(EntityTypeBuilder<FreeRecordEntity> builder)
    {
        builder.HasOne(c => c.Cabinet)
            .WithMany(c => c.FreeRecords);
    }
}