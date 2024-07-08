using ServiceTrackHub.Application.DTOS;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetServices();
        Task<ServiceDTO> GetById(int? id);
        Task<ServiceDTO> Create(ServiceDTO serviceDTO);
        Task<ServiceDTO> Update(ServiceDTO serviceDTO);
        Task<ServiceDTO> Remove(int? id);

    }
}
