using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.Interfaces.Auth;
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
        private readonly IPasswordHasherService _passwordHasherService;


        public UserService(IUserRepository userRepository, ITasksRepository tasksRepository,
            IMapper mapper, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _tasksRepository = tasksRepository;
            _passwordHasherService = passwordHasherService;
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

            return user is not null
                ? Result<UserViewModel?>.Success(UserViewModel.ToViewModel(user))
                : Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
        }

        public async Task<Result> GetByEmail(string email)
        {
            var userEntity = await _userRepository.GetByEmail(email);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            return Result<UserViewModel?>.Success(userModel);
        }

        public async Task<Result> Create(CreateUserModel userInput)
        {
            var userExists = await _userRepository.GetByEmailAsync(userInput.Email) is not null;
            if (userExists)
                return Result.Failure(
                    CustomError.Conflict(ErrorMessage.UserEmailAlreadyExists));

            var passwordHash = _passwordHasherService.HashPassword(userInput.Password);
            if (!passwordHash.IsSuccess)
                return Result.Failure(CustomError.Conflict(ErrorMessage.UserErrorPasswordHash));

            try
            {
                var userEntity = new User(userInput.Name, userInput.Email, userInput.Password, userInput.Phone);

                userEntity.ChangePassword(passwordHash.Data);

                await _userRepository.CreateAsync(userEntity);
                var userModel = _mapper.Map<UserViewModel>(userEntity);

                return Result<UserViewModel?>.Success(userModel);
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
        }

        public async Task<Result> Update(Guid id, UpdateUserModel userInput)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));

            _mapper.Map(userInput, userEntity);
            userEntity.Update();
            await _userRepository.UpdateAsync(userEntity);

            var userModel = _mapper.Map<UserViewModel>(userEntity);


            return Result<UserViewModel?>.Success(userModel);
        }

        public async Task<Result> Activate(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));

            try
            {
                userEntity.Activate();
                await _userRepository.UpdateAsync(userEntity);
                return Result.Success();
            }
            catch (InvalidOperationException e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
        }

        public async Task<Result> Deactivate(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));

            try
            {
                userEntity.Deactivate();
                await _userRepository.UpdateAsync(userEntity);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
        }

        public async Task<Result> Remove(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            var userTasks = await _tasksRepository.GetTasksByUserIdAsync(user.Id);
            if (userTasks.Count > 0)
                return Result.Failure(CustomError.Conflict(ErrorMessage.UserCannotBeRemove));
            await _userRepository.RemoveAsync(user);
            return Result.Success();
        }
    }
}