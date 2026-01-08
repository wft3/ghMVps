using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.UserManagement;

namespace Api.Repositories;

public class UserRoleAssignmentRepository : RepositoryBase<UserRoleAssignment>, IUserRoleAssignmentRepository
{
    public UserRoleAssignmentRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
