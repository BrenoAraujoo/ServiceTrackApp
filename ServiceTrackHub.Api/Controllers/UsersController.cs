﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.Extensions;
using ServiceTrackHub.Application.InputViewModel.Auth;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.Interfaces.Auth;
using ServiceTrackHub.Application.Services.Auth;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Enums;
using ServiceTrackHub.Domain.Enums.Common.Erros;
using ServiceTrackHub.Domain.Enums.Common.Result;
using ServiceTrackHub.Domain.Enums.Entities;

namespace ServiceTrackHub.Api.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        public UsersController(IUserService userService, ITokenService tokenService, IAuthService authService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _authService = authService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("v1/users")]
        public async Task <ActionResult> GetUsers()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("v1/users/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetById(id);

            return result.IsSuccess ? 
                Ok(result):
                ApiControllerHandleResult(result);
        }
        [HttpPost("v1/users")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel userInputModel)
        {
            var result = await _userService.Create(userInputModel);
            return !result.IsSuccess ? 
                ApiControllerHandleResult(result) : 
                CreatedAtAction(nameof(Create), result);
        }
        
        [HttpPut("v1/users/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateUserModel userInput)
        {
            var result = await _userService.Update(id, userInput);
            return result.IsSuccess? NoContent(): ApiControllerHandleResult(result);
        }

        [HttpPut("v1/users/{id}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            var result = await _userService.Deactivate(id);
                return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
        [HttpPut("v1/users/{id}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid id)
        {
            var result = await _userService.Activate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();

        }

        [HttpDelete("v1/users/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.Remove(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result) : NoContent();
        }

        [HttpPost("v1/users/token")]
        public async Task<IActionResult> GetUserToken([FromBody] LoginModel loginModel)
        {
            var  result = await _authService.AuthenticateAsync(loginModel);
            return !result.IsSuccess ? ApiControllerHandleResult(result) : Ok(result);
        }
 
    }
}
