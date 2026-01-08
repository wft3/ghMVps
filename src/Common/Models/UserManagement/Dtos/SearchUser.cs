using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.UserManagement.Dtos;

public class SearchUser
{
    public string? UserIdOrName { get; set; }
    public string? Program { get; set; }
    public string? Jurisdiction { get; set; }
    public string? ConditionCode { get; set; }
    public string? Email { get; set; }
}
