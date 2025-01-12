using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.CustomExceptions;
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
        
        public async Task<Result> Create(CreateTaskTypeModel taskTypeInput, Guid userId)
        {
            
            var taskTypeExists = await _taskTypeRepository.GetByNameAsync(taskTypeInput.Name) is not null;
            var user = await _userRepository.GetByIdAsync(userId);
             if(taskTypeExists)
                return Result.Failure(CustomError.Conflict(ErrorMessage.TaskNameAlreadyExists));
             
             if(user is null)
                 return Result.Failure(CustomError.Conflict(ErrorMessage.UserNotFound));
             try
             {
                 var taskTypeEntity = new TaskType(
                     userId,
                     taskTypeInput.Name,
                     taskTypeInput.Description);

                 await _taskTypeRepository.CreateAsync(taskTypeEntity);
                 var taskViewModel = TaskTypeDetailedViewModel.ToViewModel(taskTypeEntity, user);
                 return Result<TaskTypeDetailedViewModel>.Success(taskViewModel);
             }
             catch (ArgumentException e)
             {
                 return Result.Failure(CustomError.ValidationError(e.Message));
             }
             catch (Exception e)
             {
                 return Result.Failure(CustomError.ServerError("Ocorreu um erro inesperado ao criar o usuário."));
             }

        }

        public async Task<Result> Delete(Guid id)
        {
            var taskType = await _taskTypeRepository.GetByIdAsync(id);
            if (taskType is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));

            try
            {
                await _taskTypeRepository.RemoveAsync(taskType);
                return Result.Success();
            }
            catch (CustomConflictException e)
            {
                return Result.Failure(CustomError.Conflict(e.Message));
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ServerError(@$"{e.Message} - {e.StackTrace}"));
            }
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
            var user = await _userRepository.GetByIdAsync(taskTypeEntity.CreatorId); 
            if(user is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            
            taskTypeEntity.Update(taskTypeInput.Name, taskTypeInput.Description, taskTypeInput.Active);
            
            await _taskTypeRepository.UpdateAsync(taskTypeEntity);
            var taskTypeModel = TaskTypeDetailedViewModel.ToViewModel(taskTypeEntity, user);

            return Result<TaskTypeDetailedViewModel?>.Success(taskTypeModel);
        }
        
    }
}
