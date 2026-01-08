using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Lookups;

public class Profile : BaseModel
{
    public string Name { get; set; }
    public string? DisplayName { get; set; }
    public string? Version { get; set; }
    public int IsConditionSpecific { get; set; }
}
