namespace MedicalClinics.Application.CustomExceptions;

public class LoginIsNotUniqueException : Exception
{
    public LoginIsNotUniqueException(string login) :
        base($"The login {login} is not a unique"){}
}