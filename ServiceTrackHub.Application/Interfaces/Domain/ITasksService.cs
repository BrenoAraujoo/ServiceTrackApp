using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.ViewModel.Tasks;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Result> GetAll();
        Task<Result> GetById(Guid id);
        Task<Result> GetTasksByUserId(Guid userId);
        Task<Result> Create(CreateTaskModel taskInputModel);
        Task<Result> Update(Guid taskId, UpdateTaskModel taskModel);
        Task <Result>Delete(Guid id);

    }
}
