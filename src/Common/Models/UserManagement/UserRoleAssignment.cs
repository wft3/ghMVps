using Common.Models.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.UserManagement;

public class UserRoleAssignment : BaseModel
{
    public int UserRoleId { get; set; }
    public int? ProgramId { get; set; }
    public int? JurisdictionId { get; set; }
    public int? ConditionId { get; set; }

    public UserRole UserRole { get; set; } = new();
    public ProgramDto? Program { get; set; }
    public Jurisdiction? Jurisdiction { get; set; }
    public Condition? Condition { get; set; }

}
