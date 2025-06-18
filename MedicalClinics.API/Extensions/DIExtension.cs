using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Services;

namespace MedicalClinics.API.Extensions;

public static class DIExtension
{
    public static void AddCustomService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClinicService, ClinicService>();
        serviceCollection.AddScoped<ICabinetService, CabinetService>();
        serviceCollection.AddScoped<IFreeRecordService, FreeFreeRecordService>();
    }
}