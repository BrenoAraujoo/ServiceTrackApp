using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks> > GetAllAsync();
        Task<Tasks> GetByIdAsync(int? id);

        Task<IEnumerable<Tasks>> GetServicesByUserIdAsync(int userId);
        Task<Tasks> CreateAsync(Tasks tasks);
        Task<Tasks> UpdateAsync(Tasks tasks);
        Task<Tasks> RemoveAsync(Tasks tasks);
    }
}
