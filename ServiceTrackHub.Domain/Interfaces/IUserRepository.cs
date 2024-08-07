using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid? id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> RemoveAsync(User user);

    }
}
