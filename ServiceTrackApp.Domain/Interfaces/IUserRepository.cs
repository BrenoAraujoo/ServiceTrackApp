using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Pagination;

namespace ServiceTrackApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllAsync(IFilterCriteria<User> filter, PaginationRequest paginationRequest);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User trackedUser);

        Task<User?> GetByEmailAsync(string email);

        Task RemoveAsync (User user);
        
        Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime? refreshTokenExpiresAt);
        Task RemoveUserRefreshToken(User user);
    }
}
