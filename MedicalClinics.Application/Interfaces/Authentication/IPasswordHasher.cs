namespace MedicalClinics.Application.Interfaces.Authentication;

public interface IPasswordHasher
{
    string GenerateHash(string password);
    bool VerifyHash(string password, string hashedPassword);
}