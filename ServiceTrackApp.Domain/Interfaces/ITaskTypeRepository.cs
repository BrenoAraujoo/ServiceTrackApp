using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Domain.Interfaces
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
