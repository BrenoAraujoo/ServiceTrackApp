using System.Linq.Expressions;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<PagedList<Tasks>> GetAllAsync(IFilterCriteria<Tasks> filter, PaginationRequest pagination);
        Task<List<Tasks>> GetFilteredAsync(Expression<Func<Tasks, bool>> predicate);
        Task<Tasks?> GetByIdAsync(Guid? id);

        Task<List<Tasks>> GetTasksByUserIdAsync(Guid? userId);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> RemoveAsync(Tasks task);
    }
}
