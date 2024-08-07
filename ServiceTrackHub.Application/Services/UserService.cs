using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Domain;
using ServiceTrackHub.Domain.Common.Result;
using ServiceTrackHub.Domain.Common.Erros;

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

        public async Task<ResponseViewModel<IEnumerable<UserViewModel>>> GetUsers()
        {
            var usersEntity = await _userRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<UserViewModel>>(usersEntity);
            return new ResponseViewModel<IEnumerable<UserViewModel>>(result);
        }

        public async Task<UserViewModel> GetById(int? id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> Create(CreateUserInputModel UserDTO)
        {
            var userEntity = _mapper.Map<User>(UserDTO);
            await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<UserViewModel>(userEntity);
            
        }

        public async Task<UserViewModel> Update(int? id, CreateUserInputModel userDTORequest)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);

            _mapper.Map(userDTORequest,userEntity);
            await  _userRepository.UpdateAsync(userEntity);
            
            return _mapper.Map<UserViewModel>(userEntity);

        }

        public async Task Delete(int? id)
        {
            var userDomain = await _userRepository.GetByIdAsync(id);
            await _userRepository.RemoveAsync(userDomain);
        }

        public async Task<Result> Teste(int? id)
        {
            var user  = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return Result.Failure(new Error("xxx","Description"));

            }
            return Result.Sucess();
        }
    }
}
