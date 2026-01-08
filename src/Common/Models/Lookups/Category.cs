using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models.Lookups;

public class Category : BaseModel
{
    [Required]
    [DisplayName("Category")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Status")]
    public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"{Description}";
    }
}
