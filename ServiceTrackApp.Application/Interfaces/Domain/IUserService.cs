using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Application.Interfaces.Domain
{
    public interface IUserService
    {
        Task<Result> GetAll(UserFilter filter, PaginationRequest userPaginationRequest);
        Task<Result> GetById(Guid id);
        Task<Result> GetByEmail(string email);
        Task<Result> Create(CreateUserModel createUserModel);
        Task<Result> Update(Guid id, UpdateUserModel updateUserModel);
        Task <Result>Deactivate(Guid id);
        Task <Result>Activate(Guid id);
        Task <Result>Delete(Guid id);
    }
}
