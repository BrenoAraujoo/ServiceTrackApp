using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.InputViewModel.Task;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Api.Controllers
{

    [ApiController]
    public class TasksController : ApiControllerBase
    {
        private readonly ITasksService _tasksService;
        
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }
        [Authorize]
        [HttpGet("v1/tasks")]
        public async Task<IActionResult> GetTasks([FromQuery] TasksFilter filter,
            [FromQuery] PaginationRequest pagination)
        {
            var result = await _tasksService.GetAll(filter,pagination);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("v1/tasks/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _tasksService.GetById(id);
            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("v1/tasks")]
        public async Task<IActionResult> Create([FromBody] CreateTaskModel taskInput)
        {
            var result = await _tasksService.Create(taskInput);
            return result.IsSuccess ?
                CreatedAtAction(nameof(Create),result) :
                ApiControllerHandleResult(result);
        }

        [HttpGet("v1/tasks/userTasks/{id}")]
        public async Task<IActionResult> GetUserTasks(Guid id)
        {
            var userTasks = await _tasksService.GetTasksByUserId(id);
            return Ok(userTasks);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("v1/tasks/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskModel inputTask)
        {
            var result = await _tasksService.Update(id, inputTask);
            return !result.IsSuccess? ApiControllerHandleResult(result) : NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("v1/tasks/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _tasksService.Delete(id);
            return !result.IsSuccess ? ApiControllerHandleResult(result) : NoContent();
        }
       
    }
}
