using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.UserManagement;

namespace Api.Repositories;

public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
