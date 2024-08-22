using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Entities;
using ServiceTrackHub.Domain.Interfaces;
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
        
        public async Task<Result> GetAll()
        {
            var usersEntity = await _userRepository.GetAllAsync();
            var users = _mapper.Map<List<UserViewModel?>>(usersEntity);

            return Result<List<UserViewModel?>>.Success(users);
        }
        public async Task<Result> GetById(Guid? id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userModel = _mapper.Map<UserViewModel?>(user);

            return userModel is not null ?
                    Result<UserViewModel?>.Success(userModel) :
                    Result.Failure(CustomError.RecordNotFound($"Usuário com id {id} não encontrado"));
        }

        public async Task<Result> Create(CreateUserInputModel userInput)
        {
            var userExists = await _userRepository.GetByEmail(userInput.Email) is not null;
            if (userExists)
                return Result.Failure(CustomError.Conflict($"Já existe um usuário com o  e-mail {userInput.Email}cadastrado"));
            
            var userEntity = _mapper.Map<User>(userInput);
            await _userRepository.CreateAsync(userEntity);
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            return Result<UserViewModel?>.Success(userModel);
            
        }

        public async Task<Result> Update(Guid? id, UpdateUserInputModel userInput)
        {

            var userEntity = await _userRepository.GetByIdAsync(id);
            if(userEntity is null)
                return Result<UserViewModel?>.Failure(CustomError.RecordNotFound($"Usuário com id {id} não encontrado"));

            _mapper.Map(userInput,userEntity);
            await  _userRepository.UpdateAsync(userEntity);
            
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            
            return Result<UserViewModel?>.Success(userModel);
            

        }

        public async Task<Result> Delete(Guid? id)
        {
            throw new NotImplementedException();
            /*
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result<UserViewModel>.Failure(ErrorMessages.NotFound(nameof(userEntity)));
            await _userRepository.RemoveAsync(userEntity);
            return Result.Success();
            */
        }
        
    }
}
