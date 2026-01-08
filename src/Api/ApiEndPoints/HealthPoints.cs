using Scalar.AspNetCore;

namespace Api.ApiEndPoints;

public static class HealthPoints
{
    static string prefixRoute = "api/Health";

    public static void RegisterHealthEndPoints(this WebApplication app)
    {
        app.MapGet($"{prefixRoute}", () => Results.Ok("API is healthy"))
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status502BadGateway)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetHealth")
            .WithDescription("Checks the health status of the API")
            .WithDisplayName("Health Check")
            .WithTags("Health")
            .WithBadge("Health", BadgePosition.After, "green");
    }
}
