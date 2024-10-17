using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Filters;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllAsync(IFilterCriteria<User> filter, PaginationRequest paginationRequest);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User trackedUser);

        Task<User?> GetByEmailAsync(string email);

        Task RemoveAsync (User user);
        
    }
}
