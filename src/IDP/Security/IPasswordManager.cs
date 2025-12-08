namespace FW.IDP.Security
{
    public interface IPasswordManager
    {
        string HashPassword(string password);

        bool VerifyPassword(string storedHash, string providedPassword);
    }
}