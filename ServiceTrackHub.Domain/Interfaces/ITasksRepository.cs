using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<List<Tasks>> GetAllAsync();
        Task<Tasks> GetByIdAsync(Guid? id);

        Task<List<Tasks>> GetServicesByUserIdAsync(Guid? userId);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> RemoveAsync(Tasks task);
    }
}
