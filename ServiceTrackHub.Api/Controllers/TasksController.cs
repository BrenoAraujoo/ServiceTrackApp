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
    public class TasksController : ApiControllerBase
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
        public async Task<ActionResult> GetTaskById(Guid id)
        {
            var result = await _tasksService.GetById(id);
            return Ok(result);
        }

        [HttpPost("v1/tasks")]
        public async Task<IActionResult> Create([FromBody] TasksInputModel taskInput)
        {
            if (!ModelState.IsValid) 
            {
                var erros = ModelState.GetErrors();
                var resultError = Result<TasksViewModel?>.Failure(CustomError.ValidationError(ErrorMessage.TaskNotFound, erros));
                return ApiControllerHandleResult(resultError);
            }
            var result = await _tasksService.Create(taskInput);
            return result.IsSuccess ?
                CreatedAtAction(nameof(Create),result) :
                ApiControllerHandleResult(result);


        }

        [HttpGet("v1/tasks/userTasks/{id}")]
        public async Task<IActionResult> GetUserTasks(Guid id)
        {
            var userTasks = await _tasksService.GetTasksByUserIdAsync(id);
            return Ok(userTasks);
        }
        /*
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
        */
    }
}
