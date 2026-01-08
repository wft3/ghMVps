using Common.Models.UserManagement;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>?> GetUsers();
        Task<User?> GetUser(string accountIdentifier);
        Task<UserRole?> GetUserRole(int userId);
        //Task<List<UserRoleAssignment>?> GetUserRoleAssignments(int userRoleId);
        //Task<List<UserSetting>?> GetUserSettings(int userId);
    }
}
