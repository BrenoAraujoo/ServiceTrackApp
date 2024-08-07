using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;

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
        public async Task<ActionResult> Create(CreateUserInputModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

                //return BadRequest(new ResponseViewModel<List<UserViewModel>>(ModelState.GetErrors()));
            }
            var result = await _userService.Create(user);
 

            return Ok(new ResponseViewModel<UserViewModel>(result));

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
