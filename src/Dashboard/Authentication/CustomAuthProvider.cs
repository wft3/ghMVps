using Dashboard.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Dashboard.Authentication;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IJwtService _tokenService;
    private readonly ILogger<CustomAuthProvider> _logger;

    public CustomAuthProvider(
        IJwtService tokenService,
        ILogger<CustomAuthProvider> logger)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var principal = await _tokenService.ValidateTokenAsync();
            if (principal != null)
            {
                _logger.LogInformation("User authenticated: {User}", principal.Identity?.Name ?? "Unknown");
                return new AuthenticationState(principal);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting authentication state");
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task<bool> LoginAsync(string accountIdentifier)
    {
        var loginResponse = await _tokenService.AuthenticateAsync(accountIdentifier);

        if (loginResponse != null)
        {
            var principal = await _tokenService.ValidateTokenAsync();
            if (principal != null)
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
                return true;
            }
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        await _tokenService.RemoveTokenAsync();
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    public void NotifyAuthStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
