using ServiceTrackHub.Application.DTOS;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<TasksDTOResponse>> GetTasks();
        Task<TasksDTOResponse> GetById(int? id);
        Task<TasksDTOResponse> Create(TasksDTORequest serviceDTO);
        Task<TasksDTOResponse> Update(int? taskId, TasksDTORequest serviceDTO);
        Task Delete(int? id);

    }
}
