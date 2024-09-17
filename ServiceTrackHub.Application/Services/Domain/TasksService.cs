using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.Tasks;
using ServiceTrackHub.Domain.Enums.Common.Erros;
using ServiceTrackHub.Domain.Enums.Common.Result;
using ServiceTrackHub.Domain.Enums.Entities;
using ServiceTrackHub.Domain.Enums.Interfaces;

namespace ServiceTrackHub.Application.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IUserRepository _userRepository;
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

        public async Task<Result> GetTasksByUserId(Guid userId)
        {
            var tasks = await _tasksRepository.GetTasksByUserIdAsync(userId);
            var tasksViewModel = _mapper.Map<List<TasksViewModel>>(tasks);
            return Result<List<TasksViewModel>>.Success(tasksViewModel);
        }

        public async Task<Result> Create(CreateTaskModel taskInput)
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
                Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskNotFound));

        }


        public async Task<Result> Delete(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            if (task is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskNotFound));

            await _tasksRepository.RemoveAsync(task);
            return Result.Success();
            
        }

        public async Task<Result> Update(Guid id, UpdateTaskModel taskInput)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            if (task is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskNotFound));
            _mapper.Map(taskInput, task);
            task.Update();
            await _tasksRepository.UpdateAsync(task);
            var taskModel = _mapper.Map<TasksViewModel>(task);
            return Result<TasksViewModel>.Success(taskModel);
        }
        
    }
}
