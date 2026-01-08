using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
{
    public SettingRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
