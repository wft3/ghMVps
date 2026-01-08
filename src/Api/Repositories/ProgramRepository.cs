using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class ProgramRepository : RepositoryBase<ProgramDto>, IProgramRepository
{
    public ProgramRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
