using System.Security.Claims;
using ServiceTrackApp.Application.ViewModel.Auth;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Application.Interfaces.Auth;

public interface ITokenService
{
    public Token GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal  GetPrincipalFromExpiredToken(string token);
}