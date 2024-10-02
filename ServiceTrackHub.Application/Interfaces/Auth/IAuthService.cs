using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<Result> AuthenticateAsync(LoginModel loginModel);
    
}