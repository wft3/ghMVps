using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models.UserManagement;

public class User : BaseModel
{
    [Required]
    [DisplayName("Account Identifier")]
    public string AccountIdentifier { get; set; } = string.Empty;

    [Required]
    [DisplayName("User Type")]
    public string UserType { get; set; } = string.Empty;

    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Email")]
    public string Email { get; set; } = string.Empty;

    [DisplayName("Status")]
    public bool IsActive { get; set; }

    public IEnumerable<UserRole>? UserRoles { get; set; }

    public override string ToString() => $"{LastName}, {FirstName} ({AccountIdentifier})";
}
