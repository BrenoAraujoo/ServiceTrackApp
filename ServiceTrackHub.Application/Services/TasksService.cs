using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _tasksRepository;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public TasksService(ITasksRepository taskRepository, IMapper mapper, IUserRepository userRepository)
        {
            _tasksRepository = taskRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        
        public async Task<Result> GetAll()
        {
            var taskDomain = await _tasksRepository.GetAllAsync();
            var taskModel = _mapper.Map<List<TasksViewModel>>(taskDomain);
            return Result<List<TasksViewModel>>.Success(taskModel);
        }

        public async Task<Result> GetTasksByUserIdAsync(Guid userId)
        {
            var tasks = await _tasksRepository.GetTasksByUserIdAsync(userId);
            var tasksViewModel = _mapper.Map<List<TasksViewModel>>(tasks);
            return Result<List<TasksViewModel>>.Success(tasksViewModel);
        }

        public async Task<Result> Create(TasksInputModel taskInput)
        {


            var taskDomain = _mapper.Map<Tasks>(taskInput);
            await _tasksRepository.CreateAsync(taskDomain);
            var taskModel = _mapper.Map<TasksViewModel>(taskDomain);
            return Result<TasksViewModel?>.Success(taskModel);
            
            
        }

        public async Task<Result> GetById(Guid id)
        {
            var tasks = await _tasksRepository.GetByIdAsync(id);
            var taksViewModel = _mapper.Map<TasksViewModel>(tasks);


            return tasks is not null ?
                Result<TasksViewModel?>.Success(taksViewModel) :
                Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.TaskNotFound, id)));

        }


        public async Task<Result> Delete(Guid id)
        {
            throw new NotImplementedException();
            /*
            var taskDomain = await _tasksRepository.GetByIdAsync(id);
            if (taskDomain is null)
                return  Result.Failure(ErrorMessages.NotFound(nameof(id)));

            await _tasksRepository.RemoveAsync(taskDomain);
            return Result.Success();
            */
        }

        public async Task<Result> Update(Guid id, TasksInputModel taskInput)
        {

            throw new NotImplementedException();
            /*
            var taskDomain = await _tasksRepository.GetByIdAsync(id);
            if(taskDomain is null)
                return Result.Failure(ErrorMessages.NotFound(nameof(id)));

            var taskModel = _mapper.Map(taskDomain, taskInput);

            await _tasksRepository.UpdateAsync(taskDomain);
            return Result.Success();
            */
        }
        
    }
}
