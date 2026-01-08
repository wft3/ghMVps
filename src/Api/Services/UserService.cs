using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using Common.Models.UserManagement;
using Common.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services;

public class UserService : IUserService
{
    #region Variable(s)
    readonly IUserRepository _userRepository;
    readonly IUserRoleRepository _userRoleRepository;
    readonly IRoleRepository _roleRepository;
    readonly ILogger _logger;
    #endregion

    #region Constructor(s)
    public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _logger = new LoggerFactory().CreateLogger(typeof(AuthorizationService));
        _roleRepository = roleRepository;
    }

    #endregion

    #region Method(s)
    public async Task<IEnumerable<User>?> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(ex, "IUserService.GetUsers failed");
            }
            return null;
        }

    }
    public async Task<User?> GetUser(string accountIdentifier)
    {
        try
        {
            var user = await _userRepository.FindAsync(u => u.AccountIdentifier.ToLower() == accountIdentifier.ToLower());
            return user;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "IUserService.GetUser failed");
            return null;
        }
    }
    public async Task<UserRole?> GetUserRole(int userId)
    {
        try
        {
            var userRole = await _userRoleRepository.FindAsync(u => u.UserId == userId && u.IsCurrentRole);
            if (userRole == null)
            {
                _logger.LogWarning($"No current role found for user with ID {userId}");
                return null;
            }
            var role = await _roleRepository.FindAsync(r => r.Id == userRole.RoleId);
            if (role == null)
            {
                _logger.LogWarning($"Role with ID {userRole.RoleId} not found for user with ID {userId}");
                return null;
            }
            userRole.Role = role;
            return userRole;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "IUserService.GetUser failed");
            return null;
        }
    }
    #endregion
}
