namespace MedicalClinics.Application.Interfaces;

public interface IAuthenticationService
{
    Task Registration(string login, string password);
    Task<string> Login(string login, string password);
}