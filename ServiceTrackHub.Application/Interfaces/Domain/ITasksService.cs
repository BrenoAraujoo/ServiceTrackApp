using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.ViewModel.Tasks;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Interfaces.Domain
{
    public interface ITasksService
    {
        Task<Result> GetAll(IFilterCriteria<Tasks> filter, PaginationRequest pagination);
        Task<Result> GetById(Guid id);
        Task<Result> GetTasksByUserId(Guid userId);
        Task<Result> Create(CreateTaskModel taskInputModel);
        Task<Result> Update(Guid taskId, UpdateTaskModel taskModel);
        Task <Result>Delete(Guid id);

    }
}
