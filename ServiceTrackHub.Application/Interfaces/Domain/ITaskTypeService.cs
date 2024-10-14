using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Interfaces.Domain
{
    public interface ITaskTypeService
    {
        Task<Result> GetAll(PaginationRequest paginationRequest);
        Task<Result> GetById(Guid id);
        Task<Result> Create(CreateTaskTypeModel taskTypeModel);
        Task<Result> Update(Guid id, UpdateTaskTypeModel taskTypeInput);
        Task<Result> Delete(Guid id);
    }
}
