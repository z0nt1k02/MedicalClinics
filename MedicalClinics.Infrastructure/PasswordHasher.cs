using MedicalClinics.Application.Interfaces.Authentication;

namespace MedicalClinics.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyHash(string pasaword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(pasaword, hashedPassword);
    }
}