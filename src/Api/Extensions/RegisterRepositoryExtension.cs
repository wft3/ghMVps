using Api.Repositories;
using Api.Repositories.Interfaces;

namespace Api.Extensions;

public static class RegisterRepositoryExtension
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        // Register your repositories here
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IConditionRepository, ConditionRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IJurisdictionRepository, JurisdictionRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserRoleAssignmentRepository, UserRoleAssignmentRepository>();
        services.AddScoped<IUserSettingRepository, UserSettingRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}
