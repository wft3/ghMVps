using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Authentication;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
