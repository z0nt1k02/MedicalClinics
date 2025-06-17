using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class FreeRecordConfiguration : IEntityTypeConfiguration<FreeRecord>
{
    public void Configure(EntityTypeBuilder<FreeRecord> builder)
    {
        builder.HasOne(c => c.Cabinet)
            .WithMany(c => c.FreeRecords);
    }
}