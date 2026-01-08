using Api.Context;
using Api.Repositories.Interfaces;
using Common.Models.Lookups;

namespace Api.Repositories;

public class ConditionRepository : RepositoryBase<Condition>, IConditionRepository
{
    public ConditionRepository(MvpsDashboardDbContext dbContext) : base(dbContext) { }
}
