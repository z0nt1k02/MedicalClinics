

using MedicalClinics.Core.Database.Entities;
using MedicalClinics.Core.Enums;

namespace MedicalClinics.Core.Entities;

public class UserEntity
{
    private UserEntity(Guid id,string login,string hashedPassword)
    {
        Id = id;
        Login = login;
        HashedPassword = hashedPassword;
    }
    
    public Guid Id { get; private set; }
    public string HashedPassword { get; set; }
    public string Login { get; private set; }
    public Role Role { get; set; }
    
    public List<RecordOnClinic> RecordsOnClinic { get; set; }

    public static UserEntity Create(Guid id,string login,string hashedPassword)
    {
        return new UserEntity(id,login,hashedPassword);
    }
}