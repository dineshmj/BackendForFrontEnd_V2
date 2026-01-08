using FW.IDP.DBAccess.Entities;

namespace FW.IDP.Security
{
    public interface IPasswordManager
    {
        string HashPassword(User user, string password);

        bool VerifyPassword(User user, string storedHash, string providedPassword);
    }
}