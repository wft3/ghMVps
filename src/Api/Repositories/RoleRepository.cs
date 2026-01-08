using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
