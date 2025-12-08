using FW.IDP.DBAccess.Entities;

namespace FW.IDP.DBAccess;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username);

    Task<User?> FindBySubjectIdAsync(string subjectId);

    Task<IEnumerable<string>> GetRolesByUserIDAsync(int userId);

    Task<bool> ValidateCredentialsAsync(string username, string password);
}