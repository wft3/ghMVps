using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.UserManagement;

namespace Api.Repositories;

public class UserSettingRepository : RepositoryBase<UserSetting>, IUserSettingRepository
{
    public UserSettingRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
