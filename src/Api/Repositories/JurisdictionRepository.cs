using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class JurisdictionRepository : RepositoryBase<Jurisdiction>, IJurisdictionRepository
{
    public JurisdictionRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
