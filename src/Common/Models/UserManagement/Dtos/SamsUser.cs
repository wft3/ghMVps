using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.UserManagement.Dtos;

public class SamsUser
{
    /// <summary>
    /// Primary Key for Sams Database. 
    /// </summary>
    public int UserAccountNbr { get; set; }

    /// <summary>
    /// Sams Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    public string GivenName { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string CurrentSAMSStatus { get; set; } = string.Empty;
    public string ActivityName { get; set; } = string.Empty;
    public DateTime UserTerminationDate { get; set; }

    public static explicit operator User(SamsUser obj)
    {
        bool isActive = (obj.CurrentSAMSStatus.Equals("Active", StringComparison.InvariantCultureIgnoreCase) || obj.UserTerminationDate >= DateTime.Now);

        return new User
        {
            AccountIdentifier = $"{obj.UserAccountNbr}",
            Email = obj.Email,
            FirstName = obj.GivenName,
            LastName = obj.SurName,
            IsActive = isActive,
        };
    }
}
