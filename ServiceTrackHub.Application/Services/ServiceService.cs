using AutoMapper;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services
{
    public class ServiceService : IServiceService
    {
        private IServiceRepository _serviceRepository;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IUserRepository userRepository)
        {
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ServiceDTO>> GetServices()
        {
            return null;
        }
        public async Task<ServiceDTO> Create(ServiceDTO serviceDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceDTO> GetById(int? id)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceDTO> Remove(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceDTO> Update(ServiceDTO serviceDTO)
        {
            throw new NotImplementedException();
        }
    }
}
