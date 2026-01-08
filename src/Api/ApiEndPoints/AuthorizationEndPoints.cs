using Api.Services.Interfaces;
using Common.Models.Lookups;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using System.Security.Claims;

namespace Api.ApiEndPoints;

public static class AuthorizationEndPoints
{
    #region Variable(s)
    static string prefixRoute = "api/Authorization";
    #endregion

    #region Method(s)
    /// <summary>
    /// Registers all Authoriation Endpoints
    /// </summary>
    /// <param name="app"></param>
    public static void RegisterAuthorizationEndPoints(this WebApplication app)
    {
        var group = app.MapGroup(prefixRoute)
            .WithTags("Authorization")
            .WithBadge("Authorization", BadgePosition.After, "red");

        group.MapGet("/{accountIdentifier}/Claims", GetClaims)
            .Produces<IEnumerable<Claim>?>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetUserClaims")
            .WithDescription("Get all Claims")
            .WithDisplayName("Get Claims");

        group.MapGet("/{accountIdentifier}/Token", GetToken)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetToken")
            .WithDescription("Get user Token")
            .WithDisplayName("Get Token");


        //Get Token
        //app.MapGet($"{prefixRoute}", async (string userId, [FromServices] IAuthorizationService service) =>
        //{
        //    var token = await service.GenerateUserToken(userId);
        //    if (string.IsNullOrEmpty(token))
        //        return Results.NotFound("user not found");
        //    return Results.Ok(token);
        //}).Produces<string>(StatusCodes.Status200OK)
        //.Produces(StatusCodes.Status401Unauthorized)
        //.Produces(StatusCodes.Status500InternalServerError)
        //.WithName("GetAuthorization")
        //.WithDescription("Get Authorization Token for User")
        //.WithDisplayName("Get Authorization Token")
        //.WithTags("Authorization")
        //.WithBadge("Authorization", BadgePosition.After, "blue");
    }

    private static async Task<Results<Ok<string>, NotFound>> GetToken(string accountIdentifier, IAuthorizationService service)
    {
        var results = await service.GenerateUserToken(accountIdentifier);
        return !string.IsNullOrEmpty(results)
            ? TypedResults.Ok(results)
            : TypedResults.NotFound();
    }

    private static async Task<Results<Ok<List<Claim>>, NotFound>> GetClaims(string accountIdentifier, IAuthorizationService service)
    {
        var results = await service.GetClaims(accountIdentifier);
        return results is not null && results.Any()
            ? TypedResults.Ok(results)
            : TypedResults.NotFound();
    }
    #endregion
}
