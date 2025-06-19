using MedicalClinics.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalClinics.Core.Configurations;

public class RecordOnClinicConfiguration : IEntityTypeConfiguration<RecordOnClinic>
{
    public void Configure(EntityTypeBuilder<RecordOnClinic> builder)
    {
        
    }
}