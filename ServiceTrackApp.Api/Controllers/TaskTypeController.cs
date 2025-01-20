using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.TaskType;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Application.Extensions;
using ServiceTrackApp.Application.Interfaces;

namespace ServiceTrackApp.Api.Controllers
{
    [ApiController]
    public class TaskTypeController : ApiControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        private readonly ITokenService _tokenService;
        private readonly IUserContextService _userContextService;
        public TaskTypeController(ITaskTypeService taskTypeService, ITokenService tokenService, IUserContextService contextService)
        {
            _taskTypeService = taskTypeService;
            _tokenService = tokenService;
            _userContextService = contextService;
        }
        [Authorize]
        [HttpGet("v1/tasktypes")]
        public async Task<IActionResult> GetAll([FromQuery] TaskTypeFilter filter, [FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _taskTypeService.GetAll(filter, paginationRequest);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("v1/tasktypes/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskTypeService.GetById(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result) : Ok(result);
        }
        
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("v1/tasktypes")]
        public async Task<IActionResult> Create([FromBody] CreateTaskTypeModel model)
        {
            var userId = _userContextService.GetUserId();
            
            var result = await _taskTypeService.Create(model, userId);
            return result.IsSuccess ?
            CreatedAtAction(nameof(Create), result) :
            ApiControllerHandleResult(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("v1/tasktypes/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskTypeModel model, [FromRoute] Guid id)
        {
            var result = await _taskTypeService.Update(id, model);
            return result.IsSuccess? 
                NoContent():
                ApiControllerHandleResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("v1/tasktypes/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskTypeService.Delete(id);
            return !result.IsSuccess?  ApiControllerHandleResult(result): NoContent() ;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("v1/tasktypes/{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _taskTypeService.Activate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("v1/tasktypes/{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _taskTypeService.Deactivate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
    }
        
}

