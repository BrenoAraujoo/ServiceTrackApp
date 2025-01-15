using System.Security.Claims;

namespace ServiceTrackApp.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId= principal?
            .FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid result) ? result :
            throw new ArgumentNullException("User identifier is unavailable");
    }
}