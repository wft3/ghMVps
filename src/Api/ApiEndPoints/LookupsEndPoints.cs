using Api.Services.Interfaces;
using Common.Models.Lookups;
using Microsoft.AspNetCore.Http.HttpResults;
using Scalar.AspNetCore;

namespace Api.ApiEndPoints;

public static class LookupsEndPoints
{
    #region Variable(s)
    static string prefixRoute = "api/Lookups";

    #endregion

    #region Method(s)
    public static void RegisterLookupsEndPoints(this WebApplication app)
    {
        var group = app.MapGroup(prefixRoute)
            .WithTags("Lookups")
            .WithBadge("Lookups", BadgePosition.After, "blue");

        group.MapGet("/Jurisdictions", GetAllJurisdictions)
            .Produces<IEnumerable<Jurisdiction>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetJurisdictions")
            .WithDescription("Get all Jurisdictions")
            .WithDisplayName("Get Jurisdictions");
        
        group.MapGet("/Categories", GetAllCategories)
            .Produces<IEnumerable<Category>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetCategories")
            .WithDescription("Get all Categories")
            .WithDisplayName("Get Categories");

        group.MapGet("/Programs", GetAllPrograms)
            .Produces<IEnumerable<ProgramDto>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetPrograms")
            .WithDescription("Get all Programs")
            .WithDisplayName("Get Programs");

        group.MapGet("/Conditions", GetAllConditions)
            .Produces<IEnumerable<Condition>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetConditions")
            .WithDescription("Get all Conditions")
            .WithDisplayName("Get Conditions");

        group.MapGet("/Profiles", GetAllProfiles)
            .Produces<IEnumerable<Profile>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetProfiles")
            .WithDescription("Get all Profiles")
            .WithDisplayName("Get Profiles");

        group.MapGet("/Roles", GetAllRoles)
            .Produces<IEnumerable<Role>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetRoles")
            .WithDescription("Get all Roles")
            .WithDisplayName("Get Roles");
    }

    private static async Task<Results<Ok<IEnumerable<Jurisdiction>>, NoContent>> GetAllJurisdictions(ILookupService service)
    {
        var jurisdictions = await service.GetJurisdictions();
        return jurisdictions is not null && jurisdictions.Any()
            ? TypedResults.Ok(jurisdictions)
            : TypedResults.NoContent();
    }
    private static async Task<Results<Ok<IEnumerable<Category>>, NoContent>> GetAllCategories(ILookupService service)
    {
        var results = await service.GetCategories();
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NoContent();
    }
    private static async Task<Results<Ok<IEnumerable<ProgramDto>>, NoContent>> GetAllPrograms(ILookupService service)
    {
        var results = await service.GetPrograms();
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NoContent();
    }
    private static async Task<Results<Ok<IEnumerable<Condition>>, NoContent>> GetAllConditions(ILookupService service)
    {
        var results = await service.GetConditions();
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NoContent();
    }
    private static async Task<Results<Ok<IEnumerable<Profile>>, NoContent>> GetAllProfiles(ILookupService service)
    {
        var results = await service.GetProfiles();
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NoContent();
    }
    private static async Task<Results<Ok<IEnumerable<Role>>, NoContent>> GetAllRoles(ILookupService service)
    {
        var results = await service.GetRoles();
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NoContent();
    }
    #endregion
}
