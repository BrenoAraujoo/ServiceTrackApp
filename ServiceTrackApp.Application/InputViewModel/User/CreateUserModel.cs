using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Enums;
using ServiceTrackApp.Domain.ValueObjects;

namespace ServiceTrackApp.Application.InputViewModel.User
{
    public record CreateUserModel(

        string Name,
        string Email,
        string SmartPhoneNumber,
        string Password,
        string? JobPosition,
        string UserRole
    )
    {
        public Domain.Entities.User ToDomain()
        {
            if(!Enum.TryParse(UserRole, out Role role))
                throw new ArgumentException(ErrorMessage.UserRoleNotFound);
            
            return new Domain.Entities.User(
                Name,
                new Email(Email),
                Password, 
                role,
                JobPosition,
                SmartPhoneNumber);
        }
    }
    

}
