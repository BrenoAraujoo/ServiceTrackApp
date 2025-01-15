using ServiceTrackApp.Application.InputViewModel.Task;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Application.Interfaces.Domain
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
