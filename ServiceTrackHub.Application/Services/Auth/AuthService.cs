using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Enums.Common.Erros;
using ServiceTrackHub.Domain.Enums.Common.Result;
using ServiceTrackHub.Domain.Enums.Interfaces;

namespace ServiceTrackHub.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasherService _passwordHasherService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasherService = passwordHasherService;
    }
    public async Task<Result> AuthenticateAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetByEmail(loginModel.Email);
        if(user is null)
            return Result.Failure(CustomError.RecordNotFound(loginModel.Email));
        
        var checkPassword = _passwordHasherService.VerifyPassword(loginModel.Password, user.PasswordHash);
        if(!checkPassword.IsSuccess)
            return Result.Failure(checkPassword.Error);
        
        var token = _tokenService.GenerateToken(user);
        return Result<Token>.Success(token);
    }
}