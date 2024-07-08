using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Application.Interfaces;

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

        [HttpGet("v1/users/{id:int}")]
        public async Task<ActionResult> GetUserById(int? id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }
        [HttpPost("v1/users")]
        public async Task<ActionResult> Create(UserDTORequest user)
        {
            return Ok(await _userService.Create(user));

        }
        
        [HttpPut("v1/users/{id:int}")]
        public async Task<ActionResult> Update([FromRoute]int? id, [FromBody]UserDTORequest userDTO)
        {
            var user = await _userService.Update(id, userDTO);
            return Ok(user);
        }

        
    }
}
