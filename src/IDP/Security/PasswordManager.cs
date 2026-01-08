using FW.IDP.DBAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace FW.IDP.Security;

public sealed class PasswordManager
    : IPasswordManager
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string storedHash, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, storedHash, providedPassword);

        return result == PasswordVerificationResult.Success;
    }
}