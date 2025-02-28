using System.Security.Claims;

namespace Dots.Meals.Api.Extensions;

public static class ClaimsExtensions
{
    public static string GetUserId(this IEnumerable<Claim> claims)
    {
        return claims.First(k => k.Type == "userId").Value;
    }
}
