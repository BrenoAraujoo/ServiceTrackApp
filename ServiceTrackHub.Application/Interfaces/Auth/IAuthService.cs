using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<Result> AuthenticateAsync(LoginModel loginModel);
    Task<Result> Refresh(TokenRequest tokenRequest);
    
    Task<Result> Logout(TokenRequest tokenRequest);
    bool IsValidRefreshToken(User user, string providedRefreshToken);
    
}