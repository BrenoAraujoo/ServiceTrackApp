using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<Tasks> GetByIdAsync(int? id);

        Task<IEnumerable<Tasks>> GetServicesByUserIdAsync(int userId);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> RemoveAsync(Tasks task);
    }
}
