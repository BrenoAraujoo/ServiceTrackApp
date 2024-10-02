using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services.Domain
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly ITasksRepository _tasksRepository;

        public TaskTypeService(ITaskTypeRepository taskTypeRepository, ITasksRepository tasksRepository)
        {
            _taskTypeRepository = taskTypeRepository;
            _tasksRepository = tasksRepository;
        }
        
        public async Task<Result> Create(CreateTaskTypeModel taskTypeInputModel)
        {
            var taskTypeExists = await _taskTypeRepository.GetByNameAsync(taskTypeInputModel.name) is not null;

             if(taskTypeExists)
                return Result.Failure(CustomError.Conflict(ErrorMessage.TaskNameAlreadyExists));

             var taskTypeEntity = new TaskType(
                 taskTypeInputModel.creatorId,
                 taskTypeInputModel.name,
                 taskTypeInputModel.description);
             
            await _taskTypeRepository.CreateAsync(taskTypeEntity);
            var taskViewModel = TaskTypeViewModel.ToViewModel(taskTypeEntity);
            return Result<TaskTypeViewModel>.Success(taskViewModel);
        }

        public async Task<Result> Delete(Guid id)
        {
            var taskType = await _taskTypeRepository.GetByIdAsync(id);
            if (taskType is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));

            var tasks = await _tasksRepository.GetFilteredAsync(x => x.TaskTypeId == id);
            if (tasks.Count > 0)
                return Result.Failure(
                    CustomError.Conflict(ErrorMessage.TaskTypeCantBeRemoved));
            await _taskTypeRepository.RemoveAsync(taskType);
            return Result.Success();
        }

        public async Task<Result> GetAll()
        {
            
            var tasksEntity = await _taskTypeRepository.GetAllAsync();
            var tasksModel = TaskTypeViewModel.ToViewModel(tasksEntity);    
            return Result<List<TaskTypeViewModel>> .Success(tasksModel);

        }

        public async Task<Result> GetById(Guid id)
        {
            
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            if(taskTypeEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));
            
            var taskTypeEntityViewModel = TaskTypeViewModel.ToViewModel(taskTypeEntity);

            return Result<TaskTypeViewModel?>.Success(taskTypeEntityViewModel);

        }

        public async Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput)
        {
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            if(taskTypeEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));
            
            taskTypeEntity.Update(taskTypeInput.name, taskTypeInput.description);
            
            await _taskTypeRepository.UpdateAsync(taskTypeEntity);
            var taskTypeModel = TaskTypeViewModel.ToViewModel(taskTypeEntity);
            return Result<TaskTypeViewModel?>.Success(taskTypeModel);
            
        }
        
    }
}
