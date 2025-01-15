using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces.Domain;
using ServiceTrackHub.Application.ViewModel.Tasks;
using ServiceTrackHub.Domain.Common.Erros;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Services.Domain
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public TasksService(
            ITasksRepository taskRepository,
            ITaskTypeRepository taskTypeRepository,
            IUserService userService,
            IUserRepository userRepository)
        {
            _tasksRepository = taskRepository;
            _taskTypeRepository = taskTypeRepository;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<Result> GetAll(IFilterCriteria<Tasks> filter, PaginationRequest pagination)
        {
            var pagedList = await _tasksRepository.GetAllAsync(filter, pagination);
            var tasksModel = TasksViewModel.ToViewModel(pagedList.EntityList);
            var pagedViewModel = new PagedList<TasksViewModel>(tasksModel, pagedList.PageNumber, pagedList.PageSize, pagedList.TotalItems);
            return Result<PagedList<TasksViewModel>>.Success(pagedViewModel);
        }

        public async Task<Result> GetTasksByUserId(Guid userId)
        {
            var tasks = await _tasksRepository.GetTasksByUserIdAsync(userId);
            var tasksViewModel = TasksViewModel.ToViewModel(tasks);
            return Result<List<TasksViewModel>>.Success(tasksViewModel);
        }

        public async Task<Result> Create(CreateTaskModel taskInput)
        {
            try
            {
                User? userTo = null;
                if (taskInput.UserToId is not null)
                {
                    userTo = await _userRepository.GetByIdAsync((Guid)taskInput.UserToId);
                    if (userTo is null)
                        return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskUserNotFound));
                }

                var taskTypeExists = await _taskTypeRepository.GetByIdAsync(taskInput.TaskTypeId) is not null;
                if (!taskTypeExists)
                    return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskTypeNotFound));

                Tasks taskDomain = new(
                    taskInput.TaskTypeId,
                    taskInput.UserId,
                    taskInput.UserToId,
                    taskInput.Description,
                    userTo?.Active ?? false);

                await _tasksRepository.CreateAsync(taskDomain);
                var taskModel = TasksViewModel.ToViewModel(taskDomain);
                return Result<TasksViewModel>.Success(taskModel);
            }
            catch (InvalidOperationException e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
            catch (Exception)
            {
                return Result.Failure(CustomError.ServerError("Erro inesperado ao criar tarefa."));
            }
        }

        public async Task<Result> GetById(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            if (task is null)
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
            try
            {
                User? userTo = null;
                if (taskInput.UserToId is not null)
                {
                    userTo = await _userRepository.GetByIdAsync((Guid)taskInput.UserToId);
                    if (userTo is null)
                        return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskUserNotFound));
                }
                
                var task = await _tasksRepository.GetByIdAsync(id);
                if (task is null)
                    return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskNotFound));

                if (taskInput.UserToId is not null)
                {
                    var userToResult = await _userService.GetById((Guid)taskInput.UserToId);
                    if (!userToResult.IsSuccess)
                        return Result.Failure(CustomError.RecordNotFound(ErrorMessage.TaskUserNotFound));
                }

                task.Update(taskInput.UserToId, userTo?.Active??false,taskInput.Description);
                await _tasksRepository.UpdateAsync(task);
                var taskModel = TasksViewModel.ToViewModel(task);
                return Result<TasksViewModel>.Success(taskModel);
            }
            catch (ArgumentException e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ServerError("Ococorreu um erro inesperado ao criar uma tarefa"));
            }
        }
    }
}