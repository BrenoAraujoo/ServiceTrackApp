using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Parameters;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Application.Interfaces.Domain
{
    public interface IUserService
    {
        Task<Result> GetAll(UserRequestParameters userRequestParameters);
        Task<Result> GetById(Guid id);
        Task<Result> GetByEmail(string email);
        Task<Result> Create(CreateUserModel createUserModel);
        Task<Result> Update(Guid id, UpdateUserModel updateUserModel);
        Task <Result>Deactivate(Guid id);
        Task <Result>Activate(Guid id);
        Task <Result>Remove(Guid id);
    }
}
