using System.Linq.Expressions;
using ServiceTrackHub.Domain.Enums.Entities;

namespace ServiceTrackHub.Domain.Enums.Interfaces
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
