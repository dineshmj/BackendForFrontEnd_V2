using System.ComponentModel.DataAnnotations.Schema;

namespace FW.IDP.DBAccess.Entities;

[Table("UsersAndRoles")]
public sealed class UserRole
{
    // Composite Primary Key (defined in DbContext)
    public int UserID { get; set; }

    public int RoleID { get; set; }

    // Navigation Properties
    public User User { get; set; } = default!;

    public Role Role { get; set; } = default!;
}