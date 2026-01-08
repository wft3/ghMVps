using Common.Models.Lookups;

namespace Common.Models.UserManagement;

public class UserRole : BaseModel
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public bool IsCurrentRole { get; set; }
    public Role Role { get; set; } = new();
    public IEnumerable<UserRoleAssignment> UserRoleAssignments { get; set; } = new List<UserRoleAssignment>();

    public override string ToString()
    {
        return $"{Role.Name}";
    }
}
