using Api.Services.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace Api.ApiEndPoints;

public static class UserEndPoints
{
    static string prefixRoute = "api/Users";

    public static void RegisterUserEndPoints(this WebApplication app)
    {
        app.MapGet($"{prefixRoute}", async ([FromServices] IUserService service) => 
        {
            var response = await service.GetUsers();
            return Results.Ok(response);
        })
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status502BadGateway)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetUsers")
            .WithDescription("Returns a list of users")
            .WithDisplayName("Get Users")
            .WithTags("Users")
            .WithBadge("Users", BadgePosition.After, "green");
    }
}
