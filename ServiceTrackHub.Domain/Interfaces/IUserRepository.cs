using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int? id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> RemoveAsync(User user);

    }
}
