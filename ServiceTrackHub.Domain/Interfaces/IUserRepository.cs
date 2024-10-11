using ServiceTrackHub.Domain.Parameters;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Pagination;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllAsync(RequestParameters requestParameters);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmail(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task RemoveAsync (User user);
        
    }
}
