using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.ViewModel.Task;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<TasksViewModel>> GetTasks();
        Task<TasksViewModel> GetById(Guid? id);
        Task<TasksViewModel> Create(TasksInputViewModel serviceDTO);
        Task<TasksViewModel> Update(Guid? taskId, TasksInputViewModel serviceDTO);
        Task Delete(Guid? id);

    }
}
