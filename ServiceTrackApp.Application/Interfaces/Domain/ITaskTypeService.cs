using ServiceTrackApp.Application.InputViewModel.TaskType;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Application.Interfaces.Domain
{
    public interface ITaskTypeService
    {
        Task<Result> GetAll(IFilterCriteria<TaskType> filter, PaginationRequest paginationRequest);
        Task<Result> GetById(Guid id);
        Task<Result> Create(CreateTaskTypeModel taskTypeInput, Guid userId);
        Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput);
        Task<Result> Delete(Guid id);
        Task <Result>Deactivate(Guid id);
        Task <Result>Activate(Guid id);
    }
}
