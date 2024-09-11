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
        private ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, ITasksRepository tasksRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }

        public async Task<Result> GetAll()
        {
            var usersEntity = await _userRepository.GetAllAsync();
            var users = _mapper.Map<List<UserViewModel?>>(usersEntity);

            return Result<List<UserViewModel?>>.Success(users);
        }

        public async Task<Result> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userModel = _mapper.Map<UserViewModel?>(user);

            return userModel is not null
                ? Result<UserViewModel?>.Success(userModel)
                : Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.UserNotFound, id)));
        }

        public async Task<Result> Create(CreateUserModel userInput)
        {
            var userExists = await _userRepository.GetByEmailAsync(userInput.Email) is not null;
            if (userExists)
                return Result.Failure(
                    CustomError.Conflict(string.Format(ErrorMessage.UserEmailAlreadyExists, userInput.Email)));

            var userEntity = _mapper.Map<User>(userInput);
            await _userRepository.CreateAsync(userEntity);
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            return Result<UserViewModel?>.Success(userModel);
        }

        public async Task<Result> Update(Guid id, UpdateUserModel userInput)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.UserNotFound, id)));

            _mapper.Map(userInput, userEntity);
            userEntity.Update();
            await _userRepository.UpdateAsync(userEntity);

            var userModel = _mapper.Map<UserViewModel>(userEntity);


            return Result<UserViewModel?>.Success(userModel);
        }

        public async Task<Result> Deactivate(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.UserNotFound, id)));
            userEntity.Deactivate();
            await _userRepository.UpdateAsync(userEntity);
            return Result.Success();
        }

        public async Task<Result> Activate(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.UserNotFound, id)));
            userEntity.Activate();
            await _userRepository.UpdateAsync(userEntity);
            return Result.Success();
        }

        public async Task<Result> Remove(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return Result.Failure(CustomError.RecordNotFound(string.Format(ErrorMessage.UserNotFound, id)));
            var userTasks = await _tasksRepository.GetTasksByUserIdAsync(user.Id);
            if (userTasks.Count > 0)
                return Result.Failure(CustomError.Conflict(string.Format(ErrorMessage.UserCannotBeRemove, id)));
            await _userRepository.RemoveAsync(user);
            return Result.Success();
        }
    }
}