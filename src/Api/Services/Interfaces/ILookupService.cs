using Common.Models.Lookups;

namespace Api.Services.Interfaces;

public interface ILookupService
{
    Task<IEnumerable<Jurisdiction>?> GetJurisdictions();
    Task<IEnumerable<Category>?> GetCategories();
    Task<IEnumerable<ProgramDto>?> GetPrograms();
    Task<IEnumerable<Condition>?> GetConditions();
    Task<IEnumerable<Profile>?> GetProfiles();
    Task<IEnumerable<Role>?> GetRoles();
}
