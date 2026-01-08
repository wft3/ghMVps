using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models.Lookups;

public class Condition : BaseModel
{
    [Required]
    public string Code { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public int? ProgramId { get; set; }
    public int? CategoryId { get; set; }
    public int? ProfileId { get; set; }
    public bool IsHL7 { get; set; }
    public bool IsCDS { get; set; }
    public bool IsNETSS { get; set; }
    public bool IsNBS { get; set; }
    public int TimeFrame { get; set; }

    public Category? Category { get; set; }
    public ProgramDto? Program { get; set; }
    public Profile? Profile { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Code})";
    }
}
