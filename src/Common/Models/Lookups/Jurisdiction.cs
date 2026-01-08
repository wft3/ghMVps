using System.ComponentModel;

namespace Common.Models.Lookups;

public class Jurisdiction : BaseModel
{
    public string Code { get; set; }
    public string? Abbreviation { get; set; }
    public string Description { get; set; }

    [DisplayName("Status")]
    public bool IsActive { get; set; }
    public override string ToString()
    {
        return $"{Description} ({Abbreviation})";
    }
}
