using ServiceTrackHub.Application.DTOS;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<TasksDTO>> GetServices();
        Task<TasksDTO> GetById(int? id);
        Task<TasksDTO> Create(TasksDTO serviceDTO);
        Task<TasksDTO> Update(TasksDTO serviceDTO);
        Task<TasksDTO> Remove(int? id);

    }
}
