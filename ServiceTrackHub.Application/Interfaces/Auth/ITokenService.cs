using System.Security.Claims;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface ITokenService
{
    public Token GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal  GetPrincipalFromExpiredToken(string token);
}