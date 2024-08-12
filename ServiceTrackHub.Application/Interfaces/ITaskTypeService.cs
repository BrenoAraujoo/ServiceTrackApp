using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface ITaskTypeService
    {
        Task<Result> GetAll();
        Task<Result> GetById(Guid? id);
        Task<Result> Create(CreateUserInputModel UserDTO);
        Task<Result> Update(Guid? id, UpdateUserInputModel UserDTO);
        Task<Result> Delete(Guid? id);
    }
}
