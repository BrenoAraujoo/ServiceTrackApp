using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Result> GetAll();
        Task<Result> GetById(Guid id);
        Task<Result> GetTasksByUserIdAsync(Guid userId);
        Task<Result> Create(TasksInputModel taskInputModel);
        Task<Result> Update(Guid taskId, TasksInputModel serviceDTO);
        Task <Result>Delete(Guid id);

    }
}
