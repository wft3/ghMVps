using Common.Models.UserManagement;

namespace Dashboard.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
    }
}