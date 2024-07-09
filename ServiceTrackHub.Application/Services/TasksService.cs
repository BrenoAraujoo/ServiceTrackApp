using AutoMapper;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Application.Interfaces;
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


        public async Task<IEnumerable<TasksDTO>> GetServices()
        {
            return null;
        }
        public async Task<TasksDTO> Create(TasksDTO serviceDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<TasksDTO> GetById(int? id)
        {
            throw new NotImplementedException();
        }


        public async Task<TasksDTO> Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<TasksDTO> Update(TasksDTO serviceDTO)
        {
            throw new NotImplementedException();
        }
    }
}
