using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Services.Domain
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IUserRepository _userRepository;

        public TaskTypeService(
            ITaskTypeRepository taskTypeRepository,
            ITasksRepository tasksRepository,
            IUserRepository userRepository)
        {
            _taskTypeRepository = taskTypeRepository;
            _tasksRepository = tasksRepository;
            _userRepository = userRepository;
        }
        
        public async Task<Result> Create(CreateTaskTypeModel taskTypeInput)
        {
            
            var taskType = new TaskType(taskTypeInput.CreatorId,taskTypeInput.Name,taskTypeInput.Description);
            
            var taskTypeExists = await _taskTypeRepository.GetByNameAsync(taskTypeInput.Name) is not null;

             if(taskTypeExists)
                return Result.Failure(CustomError.Conflict(ErrorMessage.TaskNameAlreadyExists));

             var taskTypeEntity = new TaskType(
                 taskTypeInput.CreatorId,
                 taskTypeInput.Name,
                 taskTypeInput.Description);
             
            await _taskTypeRepository.CreateAsync(taskTypeEntity);
            var taskViewModel = TaskTypeSimpleViewModel.ToViewModel(taskTypeEntity);
            return Result<TaskTypeSimpleViewModel>.Success(taskViewModel);
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

        public async Task<Result> GetAll(IFilterCriteria<TaskType> filter, PaginationRequest paginationRequest)
        {
            var pagedList = await _taskTypeRepository.GetAllAsync(filter, paginationRequest);
            var tasksModel = TaskTypeSimpleViewModel.ToViewModel(pagedList.EntityList); 
            var pagedViewModel = new PagedList<TaskTypeSimpleViewModel>
                (tasksModel, pagedList.PageNumber, paginationRequest.PageSize, pagedList.TotalItems);
            return Result<PagedList<TaskTypeSimpleViewModel>> .Success(pagedViewModel);
        }

        public async Task<Result> GetById(Guid id)
        {
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            
            if(taskTypeEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));
            
            var userEntity = await _userRepository.GetByIdAsync(taskTypeEntity.CreatorId);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            
            var taskTypeEntityViewModel = TaskTypeDetailedViewModel.ToViewModel(taskTypeEntity, userEntity);

            return Result<TaskTypeDetailedViewModel?>.Success(taskTypeEntityViewModel);

        }

        public async Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput)
        {
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            if(taskTypeEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));
            
            taskTypeEntity.Update(taskTypeInput.Name, taskTypeInput.Description);
            
            await _taskTypeRepository.UpdateAsync(taskTypeEntity);
            var taskTypeModel = TaskTypeSimpleViewModel.ToViewModel(taskTypeEntity);
            return Result<TaskTypeSimpleViewModel?>.Success(taskTypeModel);
            
        }
        
    }
}
