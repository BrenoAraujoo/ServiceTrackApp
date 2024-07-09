using AutoMapper;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;

namespace ServiceTrackHub.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTOResponse>> GetUsers()
        {
            var usersEntity = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTOResponse>>(usersEntity);  
        }

        public async Task<UserDTOResponse> GetById(int? id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTOResponse>(user);
        }

        public async Task<UserDTOResponse> Create(UserDTORequest UserDTO)
        {
            var userEntity = _mapper.Map<User>(UserDTO);
            await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<UserDTOResponse>(userEntity);
            
        }

        public async Task<UserDTOResponse> Update(int? id, UserDTORequest userDTORequest)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);

            _mapper.Map(userDTORequest,userEntity);
            await  _userRepository.UpdateAsync(userEntity);
            
            return _mapper.Map<UserDTOResponse>(userEntity);

        }

        public async Task Delete(int? id)
        {
            var userEntity = _userRepository.GetByIdAsync(id).Result;
            await _userRepository.RemoveAsync(userEntity);
        }
    }
}
