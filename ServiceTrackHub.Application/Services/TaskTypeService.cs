using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IMapper _mapper;

        public TaskTypeService(ITaskTypeRepository taskTypeRepository, IMapper mapper)
        {
            _taskTypeRepository = taskTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result> Create(CreateTaskTypeInputModel TaskTypeInputModel)
        {
            if (TaskTypeInputModel == null)
                return Result<TaskTypeViewModel?>
                    .Failure(ErrorMessages.BadRequest(nameof(TaskTypeInputModel)));

            var taskTypeEntity = _mapper.Map<TaskType>(TaskTypeInputModel);
            await _taskTypeRepository.CreateAsync(taskTypeEntity);
            var taskTypeModel = _mapper.Map<TaskTypeViewModel>(taskTypeEntity);
            return Result<TaskTypeViewModel?> .Success(taskTypeModel);
        }

        public Task<Result> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetAll()
        {
            var tasksEntity = await _taskTypeRepository.GetAllAsync();
            var tasksModel = _mapper.Map<List<TaskTypeViewModel?>>(tasksEntity);    
            return Result<List<TaskTypeViewModel?>> .Success(tasksModel); 

        }

        public async Task<Result> GetById(Guid? id)
        {
            var taskEntity = await _taskTypeRepository.GetByIdAsync(id);
            if(taskEntity is null)
            {
                return Result<TaskTypeViewModel?>.Failure(ErrorMessages.NotFound(nameof(id)));
            }
            var taskModel = _mapper.Map<TaskType?,TaskTypeViewModel>(taskEntity); 
            return Result<TaskTypeViewModel?> .Success(taskModel);
        }

        public Task<Result> Update(Guid? id, CreateTaskTypeInputModel taskTypeModel)
        {
            throw new NotImplementedException();
        }
    }
}
