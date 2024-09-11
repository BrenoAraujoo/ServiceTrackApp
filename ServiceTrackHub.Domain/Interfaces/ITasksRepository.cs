using System.Linq.Expressions;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<List<Tasks>> GetAllAsync();
        Task<List<Tasks>> GetFilteredAsync(Expression<Func<Tasks, bool>> predicate);
        Task<Tasks?> GetByIdAsync(Guid? id);

        Task<List<Tasks>> GetTasksByUserIdAsync(Guid? userId);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> RemoveAsync(Tasks task);
    }
}
