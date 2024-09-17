using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Domain.Enums.Common.Result;

namespace ServiceTrackHub.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<Result> AuthenticateAsync(LoginModel loginModel);
    
}