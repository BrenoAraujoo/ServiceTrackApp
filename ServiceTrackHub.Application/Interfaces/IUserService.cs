using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<List<UserViewModel?>>> GetUsers();
        Task<Result<UserViewModel>?> GetById(Guid? id);
        Task<Result<UserViewModel?>> Create(CreateUserInputModel UserDTO);
        Task<UserViewModel> Update(Guid? id, CreateUserInputModel UserDTO);
        Task Delete(Guid? id);
    }
}
