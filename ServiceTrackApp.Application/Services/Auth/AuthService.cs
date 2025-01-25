using System.Security.Claims;
using ServiceTrackApp.Application.InputViewModel.Auth;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.ViewModel.Auth;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.Services.Auth;

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
            return Result.Failure(CustomError.RecordNotFound(ErrorMessage.InvalidEmailOrPassword));
       
        var storedPassword = new Password(user.PasswordHash.Value, isHashed:true);
        var checkPassword = _hashService.Verify(loginModel.Password, storedPassword.Value);
        
        if(!checkPassword.IsSuccess)
            return Result.Failure(CustomError.ValidationError(ErrorMessage.InvalidEmailOrPassword));
        
        var token = _tokenService.GenerateToken(user);
        var refreshTokenHash = _hashService.Hash(token.RefreshToken);
        if(!refreshTokenHash.IsSuccess)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        //Mudar result para result <string> no uso do hash
        //user.UpdateRefreshToken(refreshTokenHash.Data);
        //await _userRepository.UpdateAsync(user);
        await _userRepository.UpdateRefreshTokenAsync(user, refreshTokenHash.Data,token.RefreshTokenExpiresAt);
        
        return Result<Token>.Success(token);
    }

    public async Task<Result> Refresh(TokenRequest tokenRequest)
    {
        
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userId is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        
        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
        if (user is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        
        var isValidRefreshToken = IsValidRefreshToken(user, tokenRequest.RefreshToken);
        if (!isValidRefreshToken)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        var newToken = _tokenService.GenerateToken(user);
        var newRefreshTokenHash = _hashService.Hash(newToken.RefreshToken);
        if(!newRefreshTokenHash.IsSuccess)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));
        
        await _userRepository.UpdateRefreshTokenAsync(user, newRefreshTokenHash.Data, newToken.RefreshTokenExpiresAt);

        return Result<Token>.Success(newToken);
    }
    public async Task<Result> Logout(TokenRequest tokenRequest)
    {
        
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (userId is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        
        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
        if (user is null) return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        
        var isValidRefreshToken = IsValidRefreshToken(user, tokenRequest.RefreshToken);
        if (!isValidRefreshToken)
            return Result.Failure(CustomError.AuthenticationError(ErrorMessage.InvalidRefreshToken));

        await _userRepository.RemoveUserRefreshToken(user);
        return Result.Success();
    }

    public bool IsValidRefreshToken(User user, string providedRefreshToken)
    {
        return user.RefreshTokenExpiresAt > DateTime.UtcNow &&
               !string.IsNullOrEmpty(user.RefreshTokenHash) &&
                _hashService.Verify(providedRefreshToken, user.RefreshTokenHash).IsSuccess;
    }

 
}