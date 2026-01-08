using Common.Models.Authentication;
using System.Security.Claims;

namespace Api.Services.Interfaces;

public interface IAuthorizationService
{
    Task<string> GenerateUserToken(string accountIdentifier);
    Task<List<Claim>?> GetClaims(string accountIdentifier);

}
