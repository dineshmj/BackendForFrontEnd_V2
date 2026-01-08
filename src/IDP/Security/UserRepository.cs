using Microsoft.EntityFrameworkCore;

using FW.IDP.DBAccess.Entities;
using FW.IDP.Security;

namespace FW.IDP.DBAccess;

public sealed class UserRepository
    : IUserRepository
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordManager _passwordManager; // Injected dependency

    public UserRepository(IdentityDbContext context, IPasswordManager passwordManager)
    {
        _context = context;
        _passwordManager = passwordManager;
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<User?> FindBySubjectIdAsync(string subjectId)
    {
        if (int.TryParse(subjectId, out int id))
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.ID == id);
        }
        return null;
    }

    public async Task<IEnumerable<string>> GetRolesByUserIDAsync(int userId)
    {
        return await _context.UsersAndRoles
            .Where(ur => ur.UserID == userId)
            .Select(ur => ur.Role.ShortName)
            .ToListAsync();
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        var user = await FindByUsernameAsync(username);

        if (user == null)
        {
            return false;
        }

        var isValidUser = _passwordManager.VerifyPassword(user, user.HashedPassword, password);

        return isValidUser;
    }
}