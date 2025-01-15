using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.TaskType;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Application.Extensions;

namespace ServiceTrackApp.Api.Controllers
{
    [ApiController]
    public class TaskTypeController : ApiControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        private readonly ITokenService _tokenService;
        public TaskTypeController(ITaskTypeService taskTypeService, ITokenService tokenService)
        {
            _taskTypeService = taskTypeService;
            _tokenService = tokenService;
        }

        [HttpGet("v1/tasktypes")]
        public async Task<IActionResult> GetAll([FromQuery] TaskTypeFilter filter, [FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _taskTypeService.GetAll(filter, paginationRequest);
            return Ok(result);
        }

        [HttpGet("v1/tasktypes/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskTypeService.GetById(id);
            if (!result.IsSuccess)
                return ApiControllerHandleResult(result);
            return Ok(result);

        }
        [HttpPost("v1/tasktypes")]
        public async Task<IActionResult> Create([FromBody] CreateTaskTypeModel model)
        {
            var token = HttpContext.Request.Headers["Bearer"].FirstOrDefault();
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var userId = principal.GetUserId();
            
            var result = await _taskTypeService.Create(model, userId);
            return result.IsSuccess ?
            CreatedAtAction(nameof(Create), result) :
            ApiControllerHandleResult(result);
        }

        [HttpPut("v1/tasktypes/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskTypeModel model, [FromRoute] Guid id)
        {
            var result = await _taskTypeService.Update(id, model);
            return result.IsSuccess? 
                NoContent():
                ApiControllerHandleResult(result);
        }

        [HttpDelete("v1/tasktypes/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskTypeService.Delete(id);
            return !result.IsSuccess?  ApiControllerHandleResult(result): NoContent() ;
        }

        [HttpPut("v1/tasktypes/{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _taskTypeService.Activate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
        
        [HttpPut("v1/tasktypes/{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _taskTypeService.Deactivate(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result): NoContent();
        }
    }
        
}

