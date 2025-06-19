using System.Security.Claims;
using MedicalClinics.Core.Entities;

namespace MedicalClinics.Application.Interfaces.Authentication;

public interface ITokenProvider
{
    string GenerateToken(UserEntity user);
}