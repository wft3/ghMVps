using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models;

public class BaseModel
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string CreatedBy { get; set; } = string.Empty;
    
    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public string? UpdatedBy { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
}
