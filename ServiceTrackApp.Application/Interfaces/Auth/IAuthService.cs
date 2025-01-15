using ServiceTrackApp.Application.InputViewModel.Auth;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;

namespace ServiceTrackApp.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<Result> AuthenticateAsync(LoginModel loginModel);
    Task<Result> Refresh(TokenRequest tokenRequest);
    
    Task<Result> Logout(TokenRequest tokenRequest);
    bool IsValidRefreshToken(User user, string providedRefreshToken);
    
}