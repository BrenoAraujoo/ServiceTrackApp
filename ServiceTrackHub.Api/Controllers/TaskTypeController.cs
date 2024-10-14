using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Api.Controllers
{
    [ApiController]
    public class TaskTypeController : ApiControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        [HttpGet("v1/tasktypes")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _taskTypeService.GetAll(paginationRequest);
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
            var result = await _taskTypeService.Create(model);
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
    }
        
}

