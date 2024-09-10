using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.Extensions;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

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

            if (!ModelState.IsValid)
            {
                var erros = ModelState.GetErrors();
                var resultError = Result<UserViewModel?>.Failure(CustomError.ValidationError(ErrorMessage.UserInvalid, erros));
                return ApiControllerHandleResult(resultError);
            }
            
            var result = await _userService.Create(userInputModel);

            return result.IsSuccess? 
                CreatedAtAction(nameof(Create),result):
                ApiControllerHandleResult(result);
        }
        
        [HttpPut("v1/users/{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid? id, [FromBody]UpdateUserModel userInput)
        {
            
          
            if (!ModelState.IsValid) 
            {
                var erros = ModelState.GetErrors();

                var resultError = Result<UserViewModel?>.Failure(CustomError.ValidationError("erro update",erros));
                return ApiControllerHandleResult(resultError);
            }
            var result = await _userService.Update(id, userInput);
            return result.IsSuccess?
                NoContent():
                ApiControllerHandleResult(result);
            
        }

        [HttpPut("v1/users/{id}/deactivate")]
        public async Task<ActionResult> Deactivate([FromRoute] Guid? id)
        {
            var result = await _userService.Deactivate(id);
                return !result.IsSuccess ?
                    BadRequest(Result.Failure(result.Error)): 
                    NoContent();
        }
        [HttpPut("v1/users/{id}/activate")]
        public async Task<ActionResult> Activate([FromRoute] Guid? id)
        {
            var result = await _userService.Activate(id);
            if (!result.IsSuccess) 
                return BadRequest(Result.Failure(result.Error));
            
            return NoContent();

        }

        [HttpDelete("v1/users/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            var result = await _userService.Remove(id);
            return result.IsSuccess ? NoContent():
                ApiControllerHandleResult(result);
        }
 
    }
}
