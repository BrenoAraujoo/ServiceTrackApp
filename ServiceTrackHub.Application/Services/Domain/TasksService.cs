using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.ViewModel.Tasks;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services.Domain
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        public TasksService(ITasksRepository taskRepository, ITaskTypeRepository taskTypeRepository)
        {
            _tasksRepository = taskRepository;
            _taskTypeRepository = taskTypeRepository;
        }
        public async Task<Result> GetAll()
        {
            var tasks = await _tasksRepository.GetAllAsync();
            var tasksModel = TasksViewModel.ToViewModel(tasks);
            return Result<List<TasksViewModel>>.Success(tasksModel);
        }

        public async Task<Result> GetTasksByUserId(Guid userId)
        {
            var tasks = await _tasksRepository.GetTasksByUserIdAsync(userId);
            var tasksViewModel = TasksViewModel.ToViewModel(tasks);
            return Result<List<TasksViewModel>>.Success(tasksViewModel);
        }

        public async Task<Result> Create(CreateTaskModel taskInput)
        {
            var taskTypeExists = await _taskTypeRepository.GetByIdAsync(taskInput.TaskTypeId) is  null;
            
            if(!taskTypeExists)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));

            Tasks taskDomain = new(
                taskInput.TaskTypeId,
                taskInput.UserId,
                taskInput.UserId,
                taskInput.Description);
            
            await _tasksRepository.CreateAsync(taskDomain);
            var taskModel = TasksViewModel.ToViewModel(taskDomain);
            return Result<TasksViewModel>.Success(taskModel);
            
        }

        public async Task<Result> GetById(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            if(task is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskNotFound));

            var taskModel = TasksViewModel.ToViewModel(task);
            return Result<TasksViewModel>.Success(taskModel);

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
           
            task.Update(taskInput.UserToId,taskInput.Description);
            await _tasksRepository.UpdateAsync(task);
            var taskModel = TasksViewModel.ToViewModel(task);
            return Result<TasksViewModel>.Success(taskModel);
        }
        
    }
}
