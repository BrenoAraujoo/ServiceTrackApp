using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.Auth;
using ServiceTrackApp.Application.InputViewModel.User;
using ServiceTrackApp.Application.Interfaces;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Api.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IUserContextService _userContextService;
        public UsersController(IUserService userService, IAuthService authService, IUserContextService userContextService)
        {
            _userService = userService;
            _authService = authService;
            _userContextService = userContextService;
        }


        [Authorize]
        [HttpGet("v1/users")]
        public async Task <ActionResult> GetUsers([FromQuery] UserFilter filter,[FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _userService.GetAll(filter, paginationRequest);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("v1/users/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetById(id);

            return result.IsSuccess ? 
                Ok(result):
                ApiControllerHandleResult(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("v1/users")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel userInputModel)
        {
            var result = await _userService.Create(userInputModel);
            return !result.IsSuccess ? 
                ApiControllerHandleResult(result) : 
                CreatedAtAction(nameof(Create), result);
        }
        
        //[Authorize(Roles = "Admin")]
        [HttpPut("v1/users/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateUserModel userInput)
        {
            var result = await _userService.Update(id, userInput);
            return result.IsSuccess? NoContent(): ApiControllerHandleResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("v1/users/{id}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            var result = await _userService.Deactivate(id);
                return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("v1/users/{id}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid id)
        {
            var result = await _userService.Activate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();

        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("v1/users/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.Delete(id);
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
