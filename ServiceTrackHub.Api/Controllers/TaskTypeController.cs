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
    public class TaskTypeController : ApiControllerBase
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
            if (!ModelState.IsValid)
            {
                var erros = ModelState.GetErrors();
                var resultError = Result<TaskTypeViewModel>.Failure(CustomError.ValidationError(ErrorMessage.TaskTypeInvalid, erros));
                return ApiControllerHandleResult(resultError);

            }
            var result = await _taskTypeService.Create(model);
            return result.IsSuccess ?
            CreatedAtAction(nameof(Create), result) :
            ApiControllerHandleResult(result);
        }

        [HttpPut("v1/tasktypes/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskTypeModel model, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.GetErrors();
                var resultError =  Result<TaskTypeViewModel>.Failure(CustomError.ValidationError(ErrorMessage.TaskTypeInvalid, erros));
                return ApiControllerHandleResult(resultError);
            }
            var result = await _taskTypeService.Update(id, model);
            return result.IsSuccess? 
                NoContent():
                ApiControllerHandleResult(result);
            
        }

       
    }
        
}

