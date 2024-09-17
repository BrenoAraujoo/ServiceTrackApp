using ServiceTrackHub.Domain.Enums.Entities;

namespace ServiceTrackHub.Domain.Enums.Interfaces
{
    public interface ITaskTypeRepository
    {
        Task<List<TaskType>> GetAllAsync();
        Task<TaskType?> GetByIdAsync(Guid? id);
        Task<TaskType?> GetByNameAsync(string name);
        Task<TaskType> CreateAsync(TaskType taskType);
        Task<TaskType> UpdateAsync(TaskType taskType);
        Task RemoveAsync(TaskType taskType);

    }
}
