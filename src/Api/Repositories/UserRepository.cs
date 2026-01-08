using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.UserManagement;

namespace Api.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
    }
}