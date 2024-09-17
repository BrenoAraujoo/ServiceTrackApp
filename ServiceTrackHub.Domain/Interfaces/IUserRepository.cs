using ServiceTrackHub.Domain.Enums.Entities;

namespace ServiceTrackHub.Domain.Enums.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmail(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task RemoveAsync (User user);
        
    }
}
