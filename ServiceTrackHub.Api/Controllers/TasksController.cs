using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.Extensions;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.Task;

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

            var result = await _tasksService.GetTasks();
            return Ok(result);

        }

        [HttpGet("v1/taksk/{id}")]
        public async Task<ActionResult> GetTaskById(Guid? id)
        {
            var result = await _tasksService.GetById(id);
            return Ok(result);
        }

        [HttpPost("v1/tasks")]
        public async Task<ActionResult> Create([FromBody] TasksInputViewModel tasksDTORequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel<string>(ModelState.GetErrors()));
            }

            var result = await _tasksService.Create(tasksDTORequest);

            return Ok(new ResponseViewModel<TasksViewModel>(result));

        }
        [HttpPut("v1/tasks/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid? id, [FromBody] TasksInputViewModel tasksDTORequest)
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
