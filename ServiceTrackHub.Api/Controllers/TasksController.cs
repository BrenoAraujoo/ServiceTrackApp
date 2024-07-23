using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Application.Interfaces;

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

        [HttpGet("v1/taksk/{id:int}")]
        public async Task<ActionResult> GetTaskById(int? id)
        {
            var result = await _tasksService.GetById(id);
            return Ok(result);
        }

        [HttpPost("v1/tasks")]
        public async Task<ActionResult> Create([FromBody] TasksDTORequest tasksDTORequest)
        {

            var result = await _tasksService.Create(tasksDTORequest);
            return Ok(result);

        }
        [HttpPut("v1/tasks/{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int? id, [FromBody] TasksDTORequest tasksDTORequest)
        {
            var task = await _tasksService.Update(id, tasksDTORequest);
            return Ok(task);
        }

        [HttpDelete("v1/tasks/{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int? id)
        {
            await _tasksService.Delete(id);
            return NoContent();
        }
    }
}
