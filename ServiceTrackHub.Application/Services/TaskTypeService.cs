using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using System.Threading.Tasks;

namespace ServiceTrackHub.Application.Services
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public TaskTypeService(ITaskTypeRepository taskTypeRepository, ITasksRepository tasksRepository,IMapper mapper)
        {
            _taskTypeRepository = taskTypeRepository;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }
        
        public async Task<Result> Create(CreateTaskTypeModel taskTypeInputModel)
        {
            var taskTypeExists = await _taskTypeRepository.GetByNameAsync(taskTypeInputModel.name) is not null;

             if(taskTypeExists)
                return Result.Failure(CustomError.Conflict(string.Format(ErrorMessage.TaskNameAlreadyExists, taskTypeInputModel.name)));

            var taskTypeEntity = _mapper.Map<TaskType>(taskTypeInputModel);
            await _taskTypeRepository.CreateAsync(taskTypeEntity);
            var taskViewModel = _mapper.Map<TaskTypeViewModel>(taskTypeInputModel);
            return Result<TaskTypeViewModel?>.Success(taskViewModel);
        }

        public async Task<Result> Delete(Guid id)
        {
            var taskType = await _taskTypeRepository.GetByIdAsync(id);
            if (taskType is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.TaskTypeNotFound, id)));

            var tasks = await _tasksRepository.GetFilteredAsync(x => x.TaskTypeId == id);
            if (tasks.Count > 0)
                return Result.Failure(
                    CustomError.Conflict(string.Format(ErrorMessage.TaskTypeCantBeRemoved, taskType.Name)));
            await _taskTypeRepository.RemoveAsync(taskType);
            return Result.Success();
        }

        public async Task<Result> GetAll()
        {
            
            var tasksEntity = await _taskTypeRepository.GetAllAsync();
            var tasksModel = _mapper.Map<List<TaskTypeViewModel?>>(tasksEntity);    
            return Result<List<TaskTypeViewModel?>> .Success(tasksModel);

        }

        public async Task<Result> GetById(Guid id)
        {
            
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            var taskTypeEntityViewModel = _mapper.Map<TaskTypeViewModel>(taskTypeEntity);

            return taskTypeEntity is not null ?
                Result<TaskTypeViewModel?>.Success(taskTypeEntityViewModel) :
                Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.TaskTypeNotFound, id)));
        }

        public async Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput)
        {
            var taskTypeEntity = await _taskTypeRepository.GetByIdAsync(id);
            if(taskTypeEntity is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.TaskTypeNotFound, id)));
            _mapper.Map(taskTypeInput, taskTypeEntity);
            await _taskTypeRepository.UpdateAsync(taskTypeEntity);
            var taskTypeModel = _mapper.Map<TaskTypeViewModel>(taskTypeEntity);
            return Result<TaskTypeViewModel?>.Success(taskTypeModel);
            
        }
        
    }
}
