using Common.Models.Authentication;
using Dashboard.Services.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dashboard.Services;

public class JwtService : IJwtService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<JwtService> _logger;
    private const string TOKEN_KEY = "authToken";
    private string? _cachedToken;

    public JwtService(
        ProtectedSessionStorage sessionStorage,
        IHttpClientFactory httpClientFactory,
        ILogger<JwtService> logger)
    {
        _sessionStorage = sessionStorage;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string?> GetTokenAsync()
    {
        // Return cached token if exists
        if (!string.IsNullOrEmpty(_cachedToken))
            return _cachedToken;

        try
        {
            var result = await _sessionStorage.GetAsync<string>(TOKEN_KEY);
            _cachedToken = result.Success ? result.Value : null;
            if(string.IsNullOrEmpty(_cachedToken))
            {
                await AuthenticateAsync("wft3");
            }
            return _cachedToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving token from storage");
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        _cachedToken = token;
        await _sessionStorage.SetAsync(TOKEN_KEY, token);
    }

    public async Task RemoveTokenAsync()
    {
        _cachedToken = null;
        await _sessionStorage.DeleteAsync(TOKEN_KEY);
    }

    public async Task<ClaimsPrincipal?> ValidateTokenAsync(string? token = null)
    {
        token ??= await GetTokenAsync();

        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Check if token is expired
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                _logger.LogWarning("Token has expired");
                await RemoveTokenAsync();
                return null;
            }

            // Extract claims from token
            var claims = jwtToken.Claims.ToList();
            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating token");
            await RemoveTokenAsync();
            return null;
        }
    }

    public async Task<LoginResponse?> AuthenticateAsync(string accountIdentifier)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ClientWithUntrustedSSL");
            //var request = new LoginRequest { Username = username, Password = password };

            var response = await client.GetAsync("/api/authorization/wft3/Token");

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    await SetTokenAsync(loginResponse.Token);
                    return loginResponse;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during authentication");
            return null;
        }
    }
}
