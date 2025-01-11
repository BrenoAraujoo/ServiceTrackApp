using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Interfaces.Domain
{
    public interface ITaskTypeService
    {
        Task<Result> GetAll(IFilterCriteria<TaskType> filter, PaginationRequest paginationRequest);
        Task<Result> GetById(Guid id);
        Task<Result> Create(CreateTaskTypeModel taskTypeInput, Guid userId);
        Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput);
        Task<Result> Delete(Guid id);
    }
}
