using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Enums.Common.Result;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result> GetAll();
        Task<Result> GetById(Guid id);
        Task<Result> GetByEmail(string email);
        Task<Result> Create(CreateUserModel createUserModel);
        Task<Result> Update(Guid id, UpdateUserModel updateUserModel);
        Task <Result>Deactivate(Guid id);
        Task <Result>Activate(Guid id);
        Task <Result>Remove(Guid id);
    }
}
