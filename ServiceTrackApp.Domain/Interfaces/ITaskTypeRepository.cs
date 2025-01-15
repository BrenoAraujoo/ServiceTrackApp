using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces
{
    public interface ITaskTypeRepository
    {
        Task<PagedList<TaskType>> GetAllAsync(IFilterCriteria<TaskType> filter, PaginationRequest paginationRequest);
        Task<TaskType?> GetByIdAsync(Guid? id);
        Task<TaskType?> GetByNameAsync(string name);
        Task<TaskType> CreateAsync(TaskType taskType);
        Task<TaskType> UpdateAsync(TaskType taskType);
        Task RemoveAsync(TaskType taskType);

    }
}
