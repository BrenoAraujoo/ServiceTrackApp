using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.Services.Auth;
using ServiceTrackHub.Application.ViewModel.Auth;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers;

[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    [HttpPost("v1/login")]
    public async Task<IActionResult> Authenticate([FromBody] LoginModel loginModel)
    {
        var  result = await _authService.AuthenticateAsync(loginModel);
        return !result.IsSuccess ? ApiControllerHandleResult(result) : Ok(result);
    }

    [HttpPost("v1/refresh")]
    public async Task<IActionResult> Refresh(string token, string refreshToken)
    {
        var result = await _authService.Refresh(token, refreshToken);
        return !result.IsSuccess ?  ApiControllerHandleResult(result) : Ok(result);
        
    }
   
}