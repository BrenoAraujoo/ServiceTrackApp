﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.Auth;
using ServiceTrackApp.Application.Interfaces.Auth;

namespace ServiceTrackApp.Api.Controllers;

[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [AllowAnonymous]
    [HttpPost("v1/login")]
    public async Task<IActionResult> Authenticate([FromBody] LoginModel loginModel)
    {
        var  result = await _authService.AuthenticateAsync(loginModel);
        return !result.IsSuccess ? ApiControllerHandleResult(result) : Ok(result);
    }

    [HttpPost("v1/logout")]
    public async Task<IActionResult> Logout([FromBody] TokenRequest tokenRequest)
    {
        var result = await _authService.Logout(tokenRequest);
        return !result.IsSuccess ? ApiControllerHandleResult(result) : NoContent();
    }

    [HttpPost("v1/refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenRequest tokenRequest)
    {
        var result = await _authService.Refresh(tokenRequest);
        return !result.IsSuccess ?  ApiControllerHandleResult(result) : Ok(result);
        
    }
   
}