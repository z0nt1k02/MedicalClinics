using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class RecordOnClinicConfiguration : IEntityTypeConfiguration<RecordOnClinicEntity>
{
    public void Configure(EntityTypeBuilder<RecordOnClinicEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Cabinet)
            .WithMany(r => r.RecordsOnClinic);
    }
}