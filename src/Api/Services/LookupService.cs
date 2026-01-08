using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using Common.Models.Lookups;

namespace Api.Services;

public class LookupService : ILookupService
{
    #region Variable(s)
    readonly IJurisdictionRepository _jurisdictionRepository;
    readonly ICategoryRepository _categoryRepository;
    readonly IProgramRepository _programRepository;
    readonly IConditionRepository _conditionRepository;
    readonly IProfileRepository _profileRepository;
    readonly IRoleRepository _roleRepository;
    readonly ILogger _logger;
    #endregion

    #region Constructor(s)
    public LookupService(IJurisdictionRepository jurisdictionRepository, ICategoryRepository categoryRepository, IProgramRepository programRepository, IConditionRepository conditionRepository, IProfileRepository profileRepository, IRoleRepository roleRepository)
    {
        _jurisdictionRepository = jurisdictionRepository;
        _logger = new LoggerFactory().CreateLogger(typeof(LookupService));
        _categoryRepository = categoryRepository;
        _programRepository = programRepository;
        _conditionRepository = conditionRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
    }
    #endregion

    #region Method(s)
    public async Task<IEnumerable<Jurisdiction>?> GetJurisdictions()
    {
        try
        {
            var results = await _jurisdictionRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetJurisdictions failed");
            return null;
        }
    }
    public async Task<IEnumerable<Category>?> GetCategories()
    {
        try
        {
            var results = await _categoryRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetCategories failed");
            return null;
        }
    }
    public async Task<IEnumerable<ProgramDto>?> GetPrograms()
    {
        try
        {
            var results = await _programRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetPrograms failed");
            return null;
        }
    }
    public async Task<IEnumerable<Condition>?> GetConditions()
    {
        try
        {
            var results = await _conditionRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetConditions failed");
            return null;
        }
    }
    public async Task<IEnumerable<Profile>?> GetProfiles()
    {
        try
        {
            var results = await _profileRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetProfiles failed");
            return null;
        }
    }
    public async Task<IEnumerable<Role>?> GetRoles()
    {
        try
        {
            var results = await _roleRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ILookupService.GetRoles failed");
            return null;
        }
    }
    #endregion
}
