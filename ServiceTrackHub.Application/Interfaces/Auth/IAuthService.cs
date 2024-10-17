using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<Result> AuthenticateAsync(LoginModel loginModel);
    Task<Result> Refresh(TokenRequest tokenRequest);
    
    Result ValidateRefreshToken(User user, string refreshToken);
    
}