using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITaskTypeService
    {
        Task<Result> GetAll();
        Task<Result> GetById(Guid? id);
        Task<Result> Create(CreateTaskTypeInputModel taskTypeModel);
        Task<Result> Update(Guid? id, CreateTaskTypeInputModel taskTypeModel);
        Task<Result> Delete(Guid? id);
    }
}
