using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service> > GetAllAsync();
        Task<Service> GetByIdAsync(int? id);

        Task<IEnumerable<Service>> GetServicesByUserIdAsync(int userId);
        Task<Service> CreateAsync(Service service);
        Task<Service> UpdateAsync(Service service);
        Task<Service> RemoveAsync(Service service);
    }
}
