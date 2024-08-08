using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("v1/users")]
        public async Task <ActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);

        }

        [HttpGet("v1/users/{id}")]
        public async Task<ActionResult> GetUserById(Guid? id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }
        [HttpPost("v1/users")]
        public async Task<ActionResult> Create(CreateUserInputModel userInputModel)
        {

            if (!ModelState.IsValid)
            {

                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                //return BadRequest(Result<UserViewModel?>.Failure(ErrorMessages.BadRequest(nameof(userInputModel))));
                return BadRequest(Result<UserViewModel?>.Failure(ErrorMessages.BadRequest(nameof(userInputModel),erros)));
               
                
            }
            var result = await _userService.Create(userInputModel);

            if(result.IsFailure)
                return BadRequest(result);
 
            return Ok(result);

        }
        
        [HttpPut("v1/users/{id}")]
        public async Task<ActionResult> Update([FromRoute]Guid? id, [FromBody]CreateUserInputModel userDTO)
        {
            var user = await _userService.Update(id, userDTO);
            return Ok(new ResponseViewModel<UserViewModel>(user));
        }

        [HttpDelete("v1/users/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid? id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
 
    }
}
