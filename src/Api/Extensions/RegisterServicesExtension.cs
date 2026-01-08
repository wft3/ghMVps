using Api.Services.Interfaces;
using Api.Services;
using Common.Services;
using Common.Services.Interfaces;

namespace Api.Extensions;

public static class RegisterServicesExtension
{

    // Register your services here
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IDecryptionService, DecryptionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILookupService, LookupService>();
    }
}
