﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalClinics.Core.Database.Entities;

namespace MedicalClinics.Core.Entities;

public class ClinicEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CabinetEntity> Cabinets { get; set; } = [];
}