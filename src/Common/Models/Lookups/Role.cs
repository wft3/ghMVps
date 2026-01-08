using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models.Lookups;

public class Role : BaseModel
{
    [Required]
    [DisplayName("Role")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Group")]
    public string? RoleGroup { get; set; } = string.Empty;
    
    [DisplayName("Admin Role")]
    public bool IsAdminRole { get; set; }
    
    [DisplayName("Status")]
    public bool IsActive { get; set; }
    
    [DisplayName("All Events")]
    public bool HasAllEvents { get; set; }
    
}
