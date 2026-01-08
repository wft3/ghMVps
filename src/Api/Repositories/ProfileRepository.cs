using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
{
    public ProfileRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
