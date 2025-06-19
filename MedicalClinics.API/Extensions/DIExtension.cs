using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Interfaces.Authentication;
using MedicalClinics.Application.Services;
using MedicalClinics.Infrastructure;

namespace MedicalClinics.API.Extensions;

public static class DIExtension
{
    public static void AddCustomService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClinicService, ClinicService>();
        serviceCollection.AddScoped<ICabinetService, CabinetService>();
        serviceCollection.AddScoped<IFreeRecordService, FreeRecordService>();
        
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();

        serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        serviceCollection.AddScoped<ITokenProvider, TokenProvider>();
    }
}