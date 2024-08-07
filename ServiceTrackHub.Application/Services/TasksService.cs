using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.Task;
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


        public async Task<IEnumerable<TasksViewModel>> GetTasks()
        {
            var taskDomain = await _tasksRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TasksViewModel>>(taskDomain);
        }
        public async Task<TasksViewModel> Create(TasksInputViewModel tasksDTORequest)
        {
            var taskDomain = _mapper.Map<Tasks>(tasksDTORequest);
            await _tasksRepository.CreateAsync(taskDomain);
            return _mapper.Map<TasksViewModel>(taskDomain);

        }

        public async Task<TasksViewModel> GetById(int? id)
        {
            var taskDomain = await _tasksRepository.GetByIdAsync(id);
            return _mapper.Map<TasksViewModel>(taskDomain) ;
        }


        public async Task Delete(int? id)
        {
            var taskDomain = await _tasksRepository.GetByIdAsync(id);
            await _tasksRepository.RemoveAsync(taskDomain);
        }

        public async Task<TasksViewModel> Update(int? taskId, TasksInputViewModel tasksDTO)
        {
            var taskDomain = await _tasksRepository.GetByIdAsync(taskId);
            _mapper.Map(taskDomain, tasksDTO);
            await _tasksRepository.UpdateAsync(taskDomain);
            return _mapper.Map<TasksViewModel>(taskDomain);

        }
    }
}
