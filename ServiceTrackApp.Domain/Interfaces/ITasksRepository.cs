using System.Linq.Expressions;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces
{
    public interface ITasksRepository
    {
        Task<PagedList<Tasks>> GetAllAsync(IFilterCriteria<Tasks> filter, PaginationRequest pagination);
        Task<List<Tasks>> GetFilteredAsync(Expression<Func<Tasks, bool>> predicate);
        Task<Tasks?> GetByIdAsync(Guid? id);

        Task<List<Tasks>> GetByUserIdAsync(Guid? userId);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> RemoveAsync(Tasks task);
    }
}
