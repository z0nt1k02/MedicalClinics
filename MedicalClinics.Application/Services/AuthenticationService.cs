using MedicalClinics.Application.CustomExceptions;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Interfaces.Authentication;
using MedicalClinics.Core.Entities;
using MedicalClinics.Core.Enums;
using Microsoft.EntityFrameworkCore;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

namespace MedicalClinics.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    
    private readonly IMedicalClinicsDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public AuthenticationService(IMedicalClinicsDbContext context)
    {
        _context = context;
    }
    public async Task Registration(string login, string password)
    {
        bool loginIsUnique = await FindLogin(login);
        if (loginIsUnique)
        {
            throw new LoginIsNotUniqueException(login);
        }
        var users = await _context.Users.CountAsync();
        var hashedPassword = _passwordHasher.GenerateHash(password);
        UserEntity newUser = UserEntity.Create(Guid.NewGuid(),login, hashedPassword);
        if (users == 0)
            newUser.Role = Role.Admin;
        else
            newUser.Role = Role.User;
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login);
        if (user == null)
            throw new AuthenticationException("Invalid login or password");
        var result = _passwordHasher.VerifyHash(password, user.HashedPassword);
        if(!result)
            throw new Exception("Invalid password");
        var token = _tokenProvider.GenerateToken(user);
        return token;

    }

    public async Task<bool> FindLogin(string login)
    {
        bool findLogin = await _context.Users.AnyAsync(x => x.Login == login);
        return findLogin;
    }
}