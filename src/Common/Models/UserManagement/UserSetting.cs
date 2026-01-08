using Common.Models.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.UserManagement;

public class UserSetting : BaseModel
{
    public int UserId { get; set; }
    public int SettingId { get; set; }
    public bool IsActive { get; set; }

    public Setting Setting { get; set; } = new();
}
