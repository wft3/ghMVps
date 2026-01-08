using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.Lookups;

public class ProgramDto : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Status")]
    public char? IsActive { get; set; }
}
