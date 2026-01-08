using Common.Models.Authentication;
using System.Security.Claims;

namespace Dashboard.Services.Interfaces;

public interface IJwtService
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token);
    Task RemoveTokenAsync();
    Task<ClaimsPrincipal?> ValidateTokenAsync(string? token = null);
    Task<LoginResponse?> AuthenticateAsync(string accountIdentifier);
}
