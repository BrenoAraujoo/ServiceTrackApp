using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.Extensions;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{

    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;
        
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet("v1/tasks")]
        public async Task<ActionResult> GetTasks()
        {

            var result = await _tasksService.GetAll();
            return Ok(result);

        }

        [HttpGet("v1/taksk/{id}")]
        public async Task<ActionResult> GetTaskById(Guid? id)
        {
            var result = await _tasksService.GetById(id);
            return Ok(result);
        }

        [HttpPost("v1/tasks")]
        public async Task<ActionResult> Create([FromBody] TasksInputModel taskInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Result<TasksViewModel?>
                    .Failure(ErrorMessages.BadRequest(nameof(taskInput),ModelState.GetErrors())));
            }

            var result = await _tasksService.Create(taskInput);
            if(result.IsFailure)
                return BadRequest(Result<TasksViewModel?>
                    .Failure(ErrorMessages.BadRequest(nameof(taskInput))));

            return Ok(Result.Success());

        }
        [HttpPut("v1/tasks/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid? id, [FromBody] TasksInputModel tasksDTORequest)
        {
            var task = await _tasksService.Update(id, tasksDTORequest);
            return Ok(task);
        }

        [HttpDelete("v1/tasks/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid? id)
        {
            await _tasksService.Delete(id);
            return NoContent();
        }
    }
}
