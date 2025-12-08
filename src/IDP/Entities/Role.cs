using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.IDP.DBAccess.Entities;

[Table("Roles")]
public sealed class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty; // Used for role claims (e.g., "products_admin")

    [MaxLength(50)]
    public string ShortName { get; set; } = string.Empty; // Optional, descriptive short name

    // Navigation property for many-to-many relationship
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}