using System.Security.Claims;
using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IHashService hashService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashService = hashService;
    }
    public async Task<Result> AuthenticateAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetByEmailAsync(loginModel.Email);
        if(user is null)
            return Result.Failure(CustomError.RecordNotFound(loginModel.Email));
        
        var checkPassword = _hashService.Verify(loginModel.Password, user.PasswordHash);
        if(!checkPassword.IsSuccess)
            return Result.Failure(CustomError.ValidationError(ErrorMessage.InvalidEmailOrPassword));
        
        var token = _tokenService.GenerateToken(user);
        var refreshTokenHash = _hashService.Hash(token.RefreshToken);
        if(!refreshTokenHash.IsSuccess)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        //Mudar result para result <string> no uso do hash
        user.SetRefreshToken(refreshTokenHash.Data);
        await _userRepository.UpdateAsync(user);
        
        return Result<Token>.Success(token);
    }

    public async Task<Result> Refresh(TokenRequest tokenRequest)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userId is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        
        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
        if (user is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));

        var savedRefreshToken = user.RefreshTokenHash;
        if (savedRefreshToken != null && !_hashService.Verify(tokenRequest.RefreshToken, savedRefreshToken).IsSuccess)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        var newToken = _tokenService.GenerateToken(user);
        var newRefreshTokenHash = _hashService.Hash(newToken.RefreshToken);
        if(!newRefreshTokenHash.IsSuccess)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        user.SetRefreshToken(newRefreshTokenHash.Data);
        await _userRepository.UpdateAsync(user);

        return Result<Token>.Success(newToken);
    }
}