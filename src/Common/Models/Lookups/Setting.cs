using System.ComponentModel.DataAnnotations;

namespace Common.Models.Lookups;

public class Setting : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public bool CanUserChange { get; set; }
    public bool IsActive { get; set; }
}
