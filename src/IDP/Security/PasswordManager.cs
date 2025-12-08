using Microsoft.AspNetCore.Identity;

namespace FW.IDP.Security;

public sealed class PasswordManager
    : IPasswordManager
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string storedHash, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, storedHash, providedPassword);

        return result == PasswordVerificationResult.Success;
    }
}