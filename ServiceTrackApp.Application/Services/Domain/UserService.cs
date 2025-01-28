using ServiceTrackApp.Application.InputViewModel.User;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Application.ViewModel.User;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;
using ServiceTrackApp.Domain.Entities;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.Filters;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Domain.Pagination;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.Services.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IHashService _hashService;

        public UserService(IUserRepository userRepository, ITasksRepository tasksRepository, 
            IHashService hashService)
        {
            _userRepository = userRepository;
            _tasksRepository = tasksRepository;
            _hashService = hashService;
        }

        public async Task<Result> GetAll(UserFilter filter, PaginationRequest userPaginationRequest)
        {
            
            var pagedList = await _userRepository.GetAllAsync(filter, userPaginationRequest);
            var usersViewModel = UserViewModel.ToViewModel(pagedList.EntityList);
            var pagedViewModel = new PagedList<UserViewModel>
                (usersViewModel, pagedList.PageNumber, pagedList.PageSize, pagedList.TotalItems);
            
            return Result<PagedList<UserViewModel>>.Success(pagedViewModel);
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
            var userEntity = await _userRepository.GetByEmailAsync(email);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            var userModel = UserViewModel.ToViewModel(userEntity);
            return Result<UserViewModel>.Success(userModel);
        }

        public async Task<Result> Create(CreateUserModel userInput)
        {
            var userExists = await _userRepository.GetByEmailAsync(userInput.Email) is not null;
            if (userExists)
                return Result.Failure(
                    CustomError.Conflict(ErrorMessage.UserEmailAlreadyExists));
            
            try
            {
                var userEntity = CreateUser(userInput);

                var passwordHash = _hashService.Hash(userInput.Password);
                if (!passwordHash.IsSuccess)
                    return Result.Failure(CustomError.Conflict(ErrorMessage.UserErrorPasswordHash));
                
                userEntity.SetPassword(new Password(passwordHash.Data));

                await _userRepository.CreateAsync(userEntity);
                var userModel = UserViewModel.ToViewModel(userEntity);

                return Result<UserViewModel>.Success(userModel);
            }
            catch (ArgumentException e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ServerError("Ocorreu um erro inesperado ao criar o usuário."));
            }
        }

        public async Task<Result> Update(Guid id, UpdateUserModel updateUserModel)
        {

            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            
            var (email, role, jobPosition, smartPhoneNumber) = CreateUserFieldsTuple(updateUserModel);

            try
            {
                userEntity.Update(
                    updateUserModel.Name,
                    email,
                    smartPhoneNumber, 
                    jobPosition,
                    role);

                if (updateUserModel.Password is not null)
                {
                    var passwordHash = _hashService.Hash(updateUserModel.Password);
                    if (!passwordHash.IsSuccess)
                        return Result.Failure(CustomError.Conflict(ErrorMessage.UserErrorPasswordHash));
                
                    userEntity.SetPassword(new Password(passwordHash.Data));
                }

                await _userRepository.UpdateAsync(userEntity);
                var userModel = UserViewModel.ToViewModel(userEntity);
                return Result<UserViewModel>.Success(userModel);
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ValidationError(e.Message));
            }
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

        public async Task<Result> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return Result.Failure(CustomError.RecordNotFound(ErrorMessage.UserNotFound));
            var userTasks = await _tasksRepository.GetByUserIdAsync(user.Id);
            if (userTasks.Count > 0)
                return Result.Failure(CustomError.Conflict(ErrorMessage.UserCannotBeRemove));
            try
            {
                await _userRepository.RemoveAsync(user);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.Conflict(e.Message));
            }
        }

        public async Task<Result> SetUserRefreshTokenHash(User user, string refreshTokenHash)
        {
            try
            {
                user.UpdateRefreshToken(refreshTokenHash);
                await _userRepository.UpdateAsync(user);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(CustomError.ServerError(e.Message));
            } 
        }
        
        
        private static User CreateUser(CreateUserModel userModel)
        {
            return new User(
                userModel.Name,
                new Email(userModel.Email),
                new Password(userModel.Password), 
                RoleExtensions.ParseRole(userModel.Role), 
                new JobPosition(userModel.JobPosition),
                new SmartPhoneNumber(userModel.SmartPhoneNumber));
        }
        
        private static (Email? email, Role? role, JobPosition? jobPosition, SmartPhoneNumber? smartPhoneNumber) CreateUserFieldsTuple(
            UpdateUserModel updateUserModel)
        {
            var email = !string.IsNullOrWhiteSpace(updateUserModel.Email)? 
                new Email(updateUserModel.Email) :
                null;
            
            var role = RoleExtensions.ParseOrNull(updateUserModel.Role);
            
            var jobPosition = !string.IsNullOrWhiteSpace(updateUserModel.JobPosition)?
                new JobPosition(updateUserModel.JobPosition)
                : null;

            var smartPhoneNumber = !string.IsNullOrWhiteSpace(updateUserModel.SmartPhoneNumber)
                ? new SmartPhoneNumber(updateUserModel.SmartPhoneNumber)
                : null;
            return (email, role, jobPosition, smartPhoneNumber);
        }

    }
}