using Microsoft.AspNetCore.Http;
using ServiceTrackApp.Application.Extensions;
using ServiceTrackApp.Application.Interfaces;
using ServiceTrackApp.Application.Interfaces.Auth;

namespace ServiceTrackApp.Application.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
    : IUserContextService
{

    public Guid GetUserId()
    {
        var token = httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault()
            ?.Replace("Bearer ","");
        if(string.IsNullOrWhiteSpace(token)) return Guid.Empty;
        var principal = tokenService.GetPrincipalFromExpiredToken(token);
        return principal.GetUserId();
    }
}