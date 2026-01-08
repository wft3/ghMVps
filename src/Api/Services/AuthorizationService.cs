using Api.Services.Interfaces;
using Common.Models.UserManagement;
using Common.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services;

public class AuthorizationService : IAuthorizationService
{
    #region Variable(s)
    readonly IDecryptionService _decryptionService;
    readonly IUserService _userService;
    readonly IConfiguration _configuration;
    readonly ILogger _logger;
    #endregion

    #region Constructor(s)
    public AuthorizationService(IDecryptionService decryptionService, IConfiguration configuration, IUserService userService)
    {
        _decryptionService = decryptionService;
        _configuration = configuration;
        _logger = new LoggerFactory().CreateLogger(typeof(AuthorizationService));
        _userService = userService;
    }
    #endregion

    #region Method(s)
    public async Task<string> GenerateUserToken(string accountIdentifier)
    {
        try
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(accountIdentifier)) return token;

            string? jwtKey = (!string.IsNullOrEmpty(_configuration["Jwt:Key"]))
                ? _configuration["Jwt:Key"]
                : string.Empty;

            if (string.IsNullOrEmpty(jwtKey))
                return token;
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
            int minutes = (!string.IsNullOrEmpty(_configuration["Jwt:ExpiryInMinutes"]))
                ? Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"])
                : 60;
            DateTime expiry = DateTime.Now.AddMinutes(minutes);
            List<Claim>? claims = await GetClaims(accountIdentifier);
            claims ??= [];

            JwtSecurityToken jwt = new(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiry,
                signingCredentials: creds
                );
            token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(ex, "Authorization failed for : {AccountIdentifier}", accountIdentifier);
            }
            return string.Empty;
        }

    }

    public async Task<List<Claim>?> GetClaims(string accountIdentifier)
    {
        try
        {
            List<Claim> claims = new();
            var userClaims = await GetUserClaims(accountIdentifier);
            if (userClaims != null)
                claims.AddRange(userClaims);
            return claims;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClaims failed for : {AccountIdentifier}", accountIdentifier);
            return null;
        }
    }
    #region Helper Method(s)
    private async Task<List<Claim>?> GetUserClaims(string accountIdentifier)
    {
        try
        {
            List<Claim> claims = new();
            var user = await _userService.GetUser(accountIdentifier);
            if (user == null)
            {
                _logger.LogWarning("User not found for : {AccountIdentifier}", accountIdentifier);
                return null;
            }
            if (!user.IsActive)
            {
                _logger.LogWarning("User is inactive for : {AccountIdentifier}", accountIdentifier);
                return null;
            }
            claims.Add(new Claim(ClaimTypes.Name, user.AccountIdentifier));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            var roleClaim = await GetRoleClaim(user.Id);
            if (roleClaim == null)
            {
                _logger.LogWarning("Role claim not found for user : {AccountIdentifier}", accountIdentifier);
                return null;
            }
            claims.Add(roleClaim);
            return claims;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClaims failed for : {AccountIdentifier}", accountIdentifier);
            return null;
        }
    }
    private async Task<Claim?> GetRoleClaim(int userId)
    {
        try
        {
            var userRole = await _userService.GetUserRole(userId);
            if (userRole == null)
            {
                _logger.LogWarning("No role found for user with ID {UserId}", userId);
                return null;
            }
            return new Claim(ClaimTypes.Role, userRole.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetRoleClaim failed for UserId : {UserId}", userId);
            return null;
        }
    }
    #endregion
    #endregion
}
