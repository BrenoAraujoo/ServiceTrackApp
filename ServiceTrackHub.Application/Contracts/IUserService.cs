using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseViewModel<IEnumerable<UserViewModel>>> GetUsers();
        Task<Result> Teste(int ? id);
        Task<UserViewModel> GetById(int? id);
        Task<UserViewModel> Create(CreateUserInputModel UserDTO);
        Task<UserViewModel> Update(int? id, CreateUserInputModel UserDTO);
        Task Delete(int? id);
    }
}
