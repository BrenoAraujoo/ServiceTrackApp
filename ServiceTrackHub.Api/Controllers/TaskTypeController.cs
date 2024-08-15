using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Application.Extensions;
using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{
    [ApiController]
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }
        [HttpGet("v1/tasktypes")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskTypeService.GetAll();
            return Ok(result);
        }
        [HttpPost("v1/tasktypes")]
        public async Task<IActionResult> Create([FromBody] CreateTaskTypeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.GetErrors();
                return BadRequest(Result<TaskTypeViewModel?>.Failure(ErrorMessages.BadRequest(nameof(model), erros)));

            }
            var result = await _taskTypeService.Create(model);
            if (result.IsFailure)
                return BadRequest(Result<TaskTypeViewModel?>.Failure(ErrorMessages.BadRequest(nameof(model))));

            return CreatedAtAction(nameof(Create), result);
        }
    }
}
